using System.Net.Security;

namespace AdventOfCode.Console.Web.Http
{
    internal class AdventOfCodeHttpClient : IAdventOfCodeHttpClient
    {
        private static readonly string scheme = "https";
        private static readonly HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (request, certificate, cetChain, policyErrors) => {
                return policyErrors == SslPolicyErrors.None;
            }
        };

        private readonly HttpClient _client;
        private readonly string _adventOfCodeHost;
        private readonly string _sessionId;

        public AdventOfCodeHttpClient(string adventOfCodeHost, string sessionId)
        {
            _adventOfCodeHost = adventOfCodeHost;
            _sessionId = sessionId;
            _client = new HttpClient(handler);
        }

        public AdventOfCodeHttpClient(
            HttpClientHandler handler,
            string adventOfCodeHost,
            string sessionId) : this(adventOfCodeHost, sessionId)
        {
            _client = new HttpClient(handler);
        }

        public string Host { get { return _adventOfCodeHost; } }
        public string SessionId { get { return _sessionId; } }

        private Uri BuildResourceUri(string resourcePath)
        {
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = scheme,
                Host = _adventOfCodeHost,
                Path = resourcePath
            };
            return uriBuilder.Uri;
        }

        public HttpRequestMessage BuildHttpGetRequestMessage(string resourcePath)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BuildResourceUri(resourcePath));
            request.Headers.Add("Host", _adventOfCodeHost);
            request.Headers.Add("Cookie", $"session={_sessionId}");
            return request;
        }

        public virtual async Task<HttpResponseMessage?> GetResourceAsync(string resourcePath)
        {
            HttpResponseMessage? response = null;
            try
            {
                var request = BuildHttpGetRequestMessage(resourcePath);
                response = await _client.SendAsync(request);
            }
            catch (HttpRequestException e)
            {
                throw new IOException($"Could not get resource at {resourcePath}", e);
            }
            return response;
        }
    }
}
