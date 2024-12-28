namespace CodeChallenger.Sdk.WebApi.Filters
{
    using Amx.Core.Domain.Models;
    using Amx.Core.Domain.Models.Api;
    using Amx.Infrastructure.Http.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ModelStateValidationActionFilter : IActionFilter
    {
        public ModelStateValidationActionFilter(IHttpRequestIdentifier httpRequestIdentifier = null)
        {
            _httpRequestIdentifier = httpRequestIdentifier;
        }

        private readonly IHttpRequestIdentifier _httpRequestIdentifier;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                var result = new ApiErrorResult(_httpRequestIdentifier?.GetIdentifier() ?? Guid.Empty);

                var errors = (from s in filterContext.ModelState
                        where s.Value.Errors.Any()
                        select new MessageDescription(s.Key,
                            string.Join(" ", s.Value.Errors.Select(x => x.ErrorMessage)),
                            MessageDescriptionType.VALIDATION))
                        .ToList();

                result.AddError(errors);

                filterContext.Result = new BadRequestObjectResult(result);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}