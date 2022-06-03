using AdventOfCode2021.CommandLineInterface.WebClient;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public class ChallengeInputRepository : IChallengeInputRepository
    {
        private static IChallengeInputRepository? instance;
        private readonly IAdventOfCodeClient _client = AdventOfCodeClient.Instance;

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

        protected ChallengeInputRepository() : this(AdventOfCodeClient.Instance)
        { }

        public ChallengeInputRepository(IAdventOfCodeClient client)
        {
            _client = client;
        }

        public static string GetNotFoundErrorMessage(int year, int day) => $"Could not find input for year {year} and day {day}.";

        public async virtual Task<string[]> FindInputLinesByYearAndDayAsync(int year, int day)
        {
            var lines = new List<string>();

            try
            {
                using Stream stream = await _client.GetPuzzleInputAsStreamAsync(year, day);
                using StreamReader reader = new(stream);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            catch (IOException e)
            {
                throw new InvalidOperationException(GetNotFoundErrorMessage(year, day), e);
            }

            return lines.ToArray();
        }
    }
}
