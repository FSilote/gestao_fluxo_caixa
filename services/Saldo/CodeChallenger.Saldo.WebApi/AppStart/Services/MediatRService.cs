namespace CodeChallenger.Saldo.WebApi.AppStart.Services
{
    using CodeChallenger.Saldo.Adapters.Repository.Query.ListarOperacoes;
    using CodeChallenger.Saldo.Adapters.Repository.Query.RecuperarOperacao;
    using CodeChallenger.Saldo.Application.UseCases.RealizarOperacao;
    using Serilog;
    using System.Diagnostics;

    public static class MediatRService
    {
        public static void ConfigureMediatR(this WebApplicationBuilder builder)
        {
            Debug.WriteLine($"{DateTime.Now.ToLocalTime()}: Loading MediatR...");
            Log.Logger.Information("Configuring MediatR.");

            try
            {
                builder.Services.AddMediatR(opt =>
                {
                    opt.RegisterServicesFromAssemblyContaining(typeof(Program));
                    opt.RegisterServicesFromAssemblyContaining<RealizarOperacaoHandler>();
                    opt.RegisterServicesFromAssemblyContaining<RecuperarOperacaoQueryHandler>();
                });
            }
            catch (Exception e)
            {
                Log.Logger.Information(e, "Cannot load assemblies to register MediatR.");
                Debug.WriteLine("Cannot load assemblies to register MediatR.");
                throw;
            }
        }
    }
}
