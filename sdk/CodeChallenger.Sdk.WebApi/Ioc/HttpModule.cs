namespace CodeChallenger.Sdk.WebApi.Ioc
{
    using Amx.Infrastructure.Http.Abstractions;
    using CodeChallenger.Sdk.WebApi.Http;
    using Autofac;

    public class HttpModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HttpHeaderResolver>()
                .As<IHttpHeaderResolver>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<TenantResolver>()
                .As<ITenantResolver>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<HttpRequestIdentifier>()
                .As<IHttpRequestIdentifier>()
                .InstancePerLifetimeScope();
        }
    }
}
