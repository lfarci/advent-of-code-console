namespace AdventOfCode2021.CommandLineInterface.Client
{
    public class AdventOfCodeHttpClient
    {

        private static AdventOfCodeHttpClient? instance;
        private static readonly string scheme = "https";
        private static readonly HttpClientHandler handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        private HttpClient _client;
        private readonly string _adventOfCodeHost;
        private readonly string _sessionId;

        AdventOfCodeHttpClient(string adventOfCodeHost, string sessionId)
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
            request.Headers.Add("Host", Environment.MachineName);
            request.Headers.Add("Cookie", $"session={_sessionId}");
            return request;
        }

        public async Task<HttpResponseMessage?> GetResourceAsync(string resourcePath)
        {
            HttpResponseMessage? response = null;
            try
            {
                var request = BuildHttpGetRequestMessage(resourcePath);
                response = await _client.SendAsync(request);
            }
            catch (HttpRequestException e)
            {
                throw new AdventOfCodeClientException($"Could not get resource at {resourcePath}", e);
            }
            return response;
        }

        public static AdventOfCodeHttpClient GetInstance(string adventOfCodeHost, string sessionId)
        {
            if (instance == null)
            {
                instance = new AdventOfCodeHttpClient(adventOfCodeHost, sessionId);
            }
            return instance;
        }
    }
}
