﻿namespace AdventOfCode.Kit.Client.Http
{
    internal class AdventOfCodeClient : IAdventOfCodeClient
    {

        private static readonly string defaultHost = "adventofcode.com"; 
        private static readonly string defaultSessionId = "";

        private static IAdventOfCodeClient? instance = null;
        private static readonly string host = Environment.GetEnvironmentVariable("AOC_HOST") ?? defaultHost;
        private static readonly string session = Environment.GetEnvironmentVariable("AOC_SESSION_ID") ?? defaultSessionId;

        private readonly IHttpRequestSender _client;

        public static IAdventOfCodeClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdventOfCodeClient();
                }
                return instance;
            }
        }

        private AdventOfCodeClient()
        {
            _client = new AdventOfCodeHttpRequestSender(host, session);
        }

        internal AdventOfCodeClient(IHttpRequestSender client)
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

        struct Request
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
