namespace AdventOfCode2021.CommandLineInterface.Client
{
    public class AdventOfCodeClient : IAdventOfCodeClient
    {

        private static IAdventOfCodeClient? instance = null;
        private static readonly string host = "adventofcode.com";
        private static readonly string session = Environment.GetEnvironmentVariable("AOC_SESSION_ID") ?? "";

        private readonly IAdventOfCodeHttpClient _client;

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
            _client = AdventOfCodeHttpClient.GetInstance(host, session);
        }

        public AdventOfCodeClient(IAdventOfCodeHttpClient client)
        {
            _client = client;
        }

        public async Task<Stream> GetCalendarPageAsStreamAsync(int year)
        {
            HttpResponseMessage? response = await _client.GetResourceAsync($"/{year}");
            if (response != null && response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            throw new AdventOfCodeClientException($"Could not request calendar par for year {year}.");
        }

        public async Task<Stream> GetDailyChallengeInputAsStreamAsync(int year, int day)
        {
            HttpResponseMessage? response = await _client.GetResourceAsync($"{year}/day/{day}/input");
            if (response != null && response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            throw new AdventOfCodeClientException($"Could not request daily challenge input for year {year} and day {day}.");
        }
    }
}
