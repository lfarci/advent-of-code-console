using AdventOfCode.Kit.Client.Web.Http;

namespace AdventOfCode.Kit.Client.Web.Resources
{
    internal class PuzzleInputRepository : IPuzzleInputRepository
    {
        private static IPuzzleInputRepository? instance;
        private readonly IAdventOfCodeClient _client = AdventOfCodeHttpClient.Instance;

        public static IPuzzleInputRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PuzzleInputRepository();
                }
                return instance;
            }
        }

        protected PuzzleInputRepository() : this(AdventOfCodeHttpClient.Instance)
        { }

        public PuzzleInputRepository(IAdventOfCodeClient client)
        {
            _client = client;
        }

        public static string GetNotFoundErrorMessage(int year, int day) => $"Could not find input for year {year} and day {day}.";

        public async virtual Task<string[]> FindByYearAndDayAsync(int year, int day)
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
                throw new IOException(GetNotFoundErrorMessage(year, day), e);
            }

            return lines.ToArray();
        }
    }
}
