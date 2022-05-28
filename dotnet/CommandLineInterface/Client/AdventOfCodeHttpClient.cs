namespace AdventOfCode2021
{
    public class AdventOfCodeHttpClient
    {

        private static AdventOfCodeHttpClient? instance;
        private static readonly string scheme = "https";
        private static readonly  HttpClientHandler handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        private HttpClient _client;
        private readonly string _adventOfCodeHost;
        private readonly string _sessionId;

        public string Host { get; } = "localhost";
        

        AdventOfCodeHttpClient(string adventOfCodeHost, string sessionId)
        {
            _adventOfCodeHost = adventOfCodeHost;
            _sessionId = sessionId;
            _client = new HttpClient(handler);
        }


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

        private HttpRequestMessage BuildHttpGetRequestMessage(string resourcePath)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BuildResourceUri(resourcePath));
            request.Headers.Add("Host", Host);
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
                Console.Error.WriteLine($"Request failure: {e.StackTrace}");
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
