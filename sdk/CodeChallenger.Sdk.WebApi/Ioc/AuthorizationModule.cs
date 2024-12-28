namespace CodeChallenger.Sdk.WebApi.Ioc
{
    using CodeChallenger.Sdk.WebApi.Authorization;
    using Autofac;

    public class AuthorizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DefaultAuthorizationResolver>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
