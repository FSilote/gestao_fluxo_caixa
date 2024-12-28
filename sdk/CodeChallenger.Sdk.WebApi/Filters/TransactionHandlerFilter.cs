namespace CodeChallenger.Sdk.WebApi.Filters
{
    using Amx.Infrastructure.Data.Abstractions.Transaction;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class TransactionHandlerFilter : IAsyncActionFilter
    {
        public TransactionHandlerFilter(ITransactionHandler transactionHandler = null)
        {
            _transactionHandler = transactionHandler;
        }

        private readonly ITransactionHandler _transactionHandler;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await BeginTransaction(context);
            
            var executed = await next.Invoke();
            
            await ResumeTransaction(executed);
        }

        private Task BeginTransaction(ActionExecutingContext context)
        {
            var isReadonly = !"POST".Equals(context.HttpContext.Request.Method, StringComparison.InvariantCultureIgnoreCase)
                && !"PUT".Equals(context.HttpContext.Request.Method, StringComparison.InvariantCultureIgnoreCase)
                && !"DELETE".Equals(context.HttpContext.Request.Method, StringComparison.InvariantCultureIgnoreCase);

            if (_transactionHandler != null)
                return _transactionHandler.BeginAsync(isReadonly);

            return Task.CompletedTask;
        }

        private Task ResumeTransaction(ActionExecutedContext executedContext)
        {
            if (_transactionHandler != null)
            {
                if (executedContext.Exception == null)
                {
                    return _transactionHandler.CommitAsync();
                }
                else
                {
                    return _transactionHandler.RollbackAsync();
                }
            }

            return Task.CompletedTask;
        }
    }
}
