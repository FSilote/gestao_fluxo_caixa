namespace CodeChallenger.Sdk.WebApi.Http
{
    using Amx.Infrastructure.Http.Abstractions;

    public class TenantResolver : ITenantResolver
    {
        public string Host { get; private set; }

        public void SetHost(string host)
        {
            this.Host = host;
        }
    }
}
