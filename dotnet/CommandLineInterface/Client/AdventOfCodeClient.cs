namespace AdventOfCode2021.CommandLineInterface.Client
{
    public class AdventOfCodeClient : IAdventOfCodeClient
    {

        private static readonly string host = "adventofcode.com";
        private static readonly string session = Environment.GetEnvironmentVariable("AOC_SESSION_ID") ?? "";

        private readonly IAdventOfCodeHttpClient _client;

        public AdventOfCodeClient()
        {
            _client = AdventOfCodeHttpClient.GetInstance(host, session);
        }

        public AdventOfCodeClient(IAdventOfCodeHttpClient client)
        {
            _client = client;
        }

        public async Task<Stream> GetDailyChallengeInputAsStreamAsync(int year, int day)
        {
            HttpResponseMessage? response = await _client.GetResourceAsync($"{year}/day/{day}/input");
            if (response != null && response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            throw new AdventOfCodeClientException($"Could not request daily challenge input for year {year} and day {day}).");
        }
    }
}
