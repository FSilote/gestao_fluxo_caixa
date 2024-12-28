namespace CodeChallenger.Lancamentos.WebApi.AppStart.Services
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System.Text.Json.Serialization;

    public static class MvcService
    {
        public static void ConfigureMvc(this WebApplicationBuilder builder, Action<FilterCollection> filters)
        {
            builder.Services.AddControllers(opt =>
            {
                // opt.Filters.Add(typeof(ModelStateValidationActionFilter));
                // opt.Filters.Add(typeof(TransactionHandlerFilter));

                if (filters != null)
                {
                    filters.Invoke(opt.Filters);
                }

                opt.EnableEndpointRouting = false;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            });

            builder.Services.AddSingleton<IConfigurationRoot>(builder.Configuration);
            builder.Services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
            builder.Services.AddHealthChecks();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
        }
    }
}
