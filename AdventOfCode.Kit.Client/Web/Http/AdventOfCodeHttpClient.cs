namespace AdventOfCode.Kit.Client.Web.Http
{
    internal class AdventOfCodeHttpClient : IAdventOfCodeClient
    {
        private readonly IHttpRequestSender _client;

        internal AdventOfCodeHttpClient(IClientConfiguration configuration)
            : this(new AdventOfCodeHttpRequestSender(configuration.Host, configuration.SessionId))
        {}

        internal AdventOfCodeHttpClient(IHttpRequestSender client)
        {
            _client = client;
        }

        public async Task<Stream> GetCalendarPageAsStreamAsync(int year)
        {
            return await GetAdventOfCodeResourceAsync(new Request
            {
                Uri = $"/{year}",
                ErrorMessage = $"Failed to request calendar page for year {year}."
            });
        }

        public async Task<Stream> GetDayPageAsStreamAsync(int year, int day)
        {
            return await GetAdventOfCodeResourceAsync(new Request
            {
                Uri = $"/{year}/day/{day}",
                ErrorMessage = $"Failed to request day page for year {year} and day {day}."
            });
        }

        public async Task<Stream> GetPuzzleInputAsStreamAsync(int year, int day)
        {
            return await GetAdventOfCodeResourceAsync(new Request
            {
                Uri = $"{year}/day/{day}/input",
                ErrorMessage = $"Failed to request puzzle input for year {year} and day {day}."
            });
        }

        private struct Request
        {
            public string Uri { get; set; }
            public string ErrorMessage { get; set; }
        }

        private async Task<Stream> GetAdventOfCodeResourceAsync(Request request)
        {
            HttpResponseMessage? response = await _client.GetResourceAsync(request.Uri);
            if (response != null && response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            throw new IOException(request.ErrorMessage);
        }
    }
}
