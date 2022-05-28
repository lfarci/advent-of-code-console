namespace AdventOfCode2021.CommandLineInterface.Client
{
    class AdventOfCodeClient : IAdventOfCodeClient
    {

        private static readonly string host = "adventofcode.com";
        private static readonly string session = Environment.GetEnvironmentVariable("AOC_SESSION_ID") ?? "";
        private static readonly AdventOfCodeHttpClient client = AdventOfCodeHttpClient.GetInstance(host, session);
        
        public async Task<Stream> GetDailyChallengeInputAsStreamAsync(int year, int day)
        {
            HttpResponseMessage? response = await client.GetResourceAsync($"{year}/day/{day}/input");
            if (response != null && response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            throw new AdventOfCodeClientException($"Could not request daily challenge input for year {year} and day {day}).");
        }
    }
}
