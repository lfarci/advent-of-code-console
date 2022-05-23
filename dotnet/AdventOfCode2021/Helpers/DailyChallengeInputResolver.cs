using AdventOfCode2021.Challenges;
using System.Reflection;

namespace AdventOfCode2021.Helpers
{

    public class DailyChallengeInputResolver
    {
        private static DailyChallengeInputResolver? _instance;
        private static readonly AdventOfCodeClient client = AdventOfCodeClient.GetInstance();

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
