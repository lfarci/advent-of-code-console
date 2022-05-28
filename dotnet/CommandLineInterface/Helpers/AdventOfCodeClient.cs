namespace AdventOfCode2021
{
    public class AdventOfCodeClient
    {

        private static AdventOfCodeClient? _instance;
        private HttpClient _client;
        public string Host { get; } = "localhost";
        public string SessionId
        {
            get
            {
                return System.Environment.GetEnvironmentVariable("AOC_SESSION_ID") ?? "";
            }
        }

        AdventOfCodeClient()
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _client = new HttpClient(handler);
        }

        private string BuildDailyChallengeInputPath(int year, int day)
        {
            return $"{year}/day/{day}/input";
        }

        private Uri BuildDailyChallengeInputUri(int year, int day)
        {
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = "adventofcode.com",
                Path = BuildDailyChallengeInputPath(year, day)
            };
            return uriBuilder.Uri;
        }

        private HttpRequestMessage BuildDailyChallengeHttpRequestMessage(int year, int day)
        {
            Uri dailyChallengeInputUri = BuildDailyChallengeInputUri(year, day);
            var request = new HttpRequestMessage(HttpMethod.Get, dailyChallengeInputUri);
            request.Headers.Add("Host", Host);
            request.Headers.Add("Cookie", $"session={SessionId}");
            return request;
        }

        private async Task<HttpResponseMessage?> SendDailyChallengeInputRequest(int year, int day)
        {
            HttpResponseMessage? response = null;
            try
            {
                var request = BuildDailyChallengeHttpRequestMessage(year, day);
                response = await _client.SendAsync(request);
            }
            catch (HttpRequestException e)
            {
                Console.Error.WriteLine($"Request failure: {e.StackTrace}");
            }
            return response;
        }

        public static AdventOfCodeClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AdventOfCodeClient();
            }
            return _instance;
        }

        public async Task<Stream> GetDailyChallengeInputAsStreamAsync(int year, int day)
        {
            HttpResponseMessage? response = await SendDailyChallengeInputRequest(year, day);
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
                else
                {
                    throw new AdventOfCodeClientException($"Error status code: {response.StatusCode}.");
                }
            }
            else
            {
                throw new AdventOfCodeClientException($"Could not request daily challenge input (year {year}, day {day}).");
            }
        }

    }

    public class AdventOfCodeClientException : Exception
    {
        public AdventOfCodeClientException()
        {
        }

        public AdventOfCodeClientException(string? message) : base(message)
        {
        }
    }
}
