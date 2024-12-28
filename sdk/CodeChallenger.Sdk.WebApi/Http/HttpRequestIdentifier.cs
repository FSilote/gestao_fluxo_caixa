namespace CodeChallenger.Sdk.WebApi.Http
{
    using Amx.Infrastructure.Http.Abstractions;

    public class HttpRequestIdentifier : IHttpRequestIdentifier
    {
        #region Ctrs

        public HttpRequestIdentifier()
        {
            _identifier = Guid.NewGuid();
        }

        #endregion

        #region Attrs

        private readonly Guid _identifier;

        #endregion

        public Guid GetIdentifier()
        {
            return _identifier;
        }
    }
}