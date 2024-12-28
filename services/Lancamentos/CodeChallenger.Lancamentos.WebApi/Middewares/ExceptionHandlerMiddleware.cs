namespace CodeChallenger.Lancamentos.WebApi.Middewares
{
    using CodeChallenger.Lancamentos.Domain.Exceptions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Serilog;
    using System.Net;
    using System.Text;

    public class ExceptionHandlerMiddleware
    {
        #region Ctrs

        public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        #endregion

        #region Attrs

        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;

        #endregion

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var originalBodyStream = httpContext.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                try
                {
                    httpContext.Response.Body = responseBody;
                    await _next(httpContext);

                    if (httpContext.Response.StatusCode == HttpStatusCode.Unauthorized.GetHashCode())
                    {
                        // TODO: Tratar acesso não autorizado
                    }
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(httpContext, ex);
                }
                finally
                {
                    httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        #region Private

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            if (ex is DomainException)
            {
                code = HttpStatusCode.BadRequest;

                _logger.Verbose(ex, "Message: {Message}", GetExceptionMessage(ex));
            }
            else if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;

                _logger.Verbose(ex, "Message: {Message}", GetExceptionMessage(ex));
            }
            else
            {
                _logger.Error(ex,
                    "Unexpected error. RequestId: {RequestId}. Message: {Message}",
                    GetExceptionMessage(ex));
            }

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var jsonObject = JsonConvert.SerializeObject(new { Error = code, Message = ex.Message  }, settings);
            await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
        }

        private string GetExceptionMessage(Exception e)
        {
            var builder = new StringBuilder();

            builder.AppendLine(e.Message);

            if (e.InnerException != null)
            {
                builder.AppendLine(GetExceptionMessage(e.InnerException));
            }

            return builder.ToString();
        }

        #endregion
    }
}
