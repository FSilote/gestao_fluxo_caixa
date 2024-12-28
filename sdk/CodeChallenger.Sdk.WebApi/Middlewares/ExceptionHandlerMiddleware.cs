namespace CodeChallenger.Sdk.WebApi.Middlewares
{
    using Amx.Core.Domain.Exceptions;
    using Amx.Core.Domain.Models;
    using Amx.Core.Domain.Models.Api;
    using Amx.Core.Domain.Resources;
    using Amx.Infrastructure.Http.Abstractions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using Serilog;
    using System.IdentityModel.Tokens.Jwt;
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
                        throwUnauthorizedException(httpContext);
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
            var result = new ApiErrorResult(httpRequestIdentifier.GetIdentifier());

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            if (ex is DomainException bex)
            {
                code = HttpStatusCode.BadRequest;

                if (bex.Errors?.Any() ?? false)
                {
                    result.AddError(bex.Errors);
                }

                _logger.Verbose(ex, "Message: {Message}", GetExceptionMessage(bex));
            }
            else if (ex is NotFoundException nfex)
            {
                code = HttpStatusCode.NotFound;

                if (nfex.Errors?.Any() ?? false)
                {
                    result.AddError(nfex.Errors);
                }

                _logger.Verbose(ex, "Message: {Message}", GetExceptionMessage(nfex));
            }
            else if (ex is UnauthorizedException unex)
            {
                code = HttpStatusCode.Unauthorized;

                if (unex.Errors?.Any() ?? false)
                {
                    result.AddError(unex.Errors);
                }

                _logger.Verbose(ex, "Message: {Message}", GetExceptionMessage(unex));
            }
            else if (ex is ForbiddenException fbex)
            {
                code = HttpStatusCode.Forbidden;

                if (fbex.Errors?.Any() ?? false)
                {
                    result.AddError(fbex.Errors);
                }

                _logger.Verbose(ex, "Message: {Message}", GetExceptionMessage(fbex));
            }
            else
            {
                var errorMessage = _env.IsDevelopment()
                    ? GetExceptionMessage(ex)
                    : CoreDomainResource.common__unexpected_error;

                _logger.Error(ex,
                    "Unexpected error. RequestId: {RequestId}. Message: {Message}",
                    httpRequestIdentifier.GetIdentifier(),
                    GetExceptionMessage(ex));

                result.AddError(new MessageDescription(
                    nameof(CoreDomainResource.common__unexpected_error),
                    errorMessage,
                    MessageDescriptionType.ERROR));
            }

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var jsonObject = JsonConvert.SerializeObject(result, settings);
            await context.Response.WriteAsync(jsonObject, Encoding.UTF8);
        }

        private void throwUnauthorizedException(HttpContext httpContext)
        {
            var authorization = httpContext.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrEmpty(authorization) && authorization.Contains("Bearer"))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(authorization.Replace("Bearer ", ""));
                var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
                var utcDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(tokenExp)).UtcDateTime;

                if (utcDateTime < DateTime.UtcNow)
                {
                    throw new UnauthorizedException(CoreDomainResource.access__unauthorized__token_expired,
                        CoreDomainResource.access__unauthorized__token_expired);
                }
            }

            throw new UnauthorizedException();
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
