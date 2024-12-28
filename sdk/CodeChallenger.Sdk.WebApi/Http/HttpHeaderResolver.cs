namespace CodeChallenger.Sdk.WebApi.Http
{
    using Amx.Infrastructure.Http.Abstractions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    public class HttpHeaderResolver : IHttpHeaderResolver
    {
        private HttpRequest? _httpRequest;

        public bool HasHeader(string header)
        {
            if (string.IsNullOrEmpty(header) || _httpRequest == null)
                return false;

            return _httpRequest.Headers.ContainsKey(header);
        }

        public string GetValue(string header)
        {
            if (string.IsNullOrEmpty(header) || _httpRequest == null || !this.HasHeader(header))
                return null!;

            return _httpRequest.Headers.TryGetValue(header, out StringValues value)
                ? value.ToString()
                : null!;
        }

        public void SetHttpRequest(object httpRequest)
        {
            _httpRequest = httpRequest as HttpRequest;
        }
    }
}
