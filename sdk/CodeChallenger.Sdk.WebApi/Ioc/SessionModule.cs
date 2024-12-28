namespace CodeChallenger.Sdk.WebApi.Ioc
{
    using Amx.Infrastructure.Http.Abstractions;
    using CodeChallenger.Sdk.WebApi.Http;
    using Autofac;

    public class SessionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TokenSession>()
                .As<ITokenSession>()
                .InstancePerLifetimeScope();
        }
    }
}
