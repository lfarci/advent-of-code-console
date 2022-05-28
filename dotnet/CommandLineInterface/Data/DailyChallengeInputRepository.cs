using AdventOfCode2021.CommandLineInterface.Client;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public static class DailyChallengeInputRepository
    {
        private static readonly IAdventOfCodeClient client = AdventOfCodeClient.Instance;

        public static async Task<Stream> GetInputAsStreamAsync(int year, int day)
        {
            return await client.GetDailyChallengeInputAsStreamAsync(year, day);
        }

        public static async Task<string[]> ReadAllLinesFrom(int year, int day)
        {
            List<string> lines = new List<string>();
            using (Stream stream = await GetInputAsStreamAsync(year, day))
            using (StreamReader reader = new StreamReader(stream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines.ToArray();
        }

    }
}
