using AdventOfCode2021.CommandLineInterface.WebClient;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public class ChallengeInputRepository : IChallengeInputRepository
    {
        private static IChallengeInputRepository? instance;
        private static readonly IAdventOfCodeClient client = AdventOfCodeClient.Instance;

        protected ChallengeInputRepository()
        {
        }

        public static IChallengeInputRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChallengeInputRepository();
                }
                return instance;
            }
        }

        public async virtual Task<Stream> FindInputStreamByYearAndDayAsync(int year, int day)
        {
            return await client.GetPuzzleInputAsStreamAsync(year, day);
        }

        public async virtual Task<string[]> FindInputLinesByYearAndDayAsync(int year, int day)
        {
            var lines = new List<string>();

            try
            {
                using Stream stream = await FindInputStreamByYearAndDayAsync(year, day);
                using StreamReader reader = new(stream);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            catch (AdventOfCodeClientException e)
            {
                throw new ArgumentOutOfRangeException($"Could not find lines for year {year} and day {day}.", e);
            }

            return lines.ToArray();
        }
    }
}
