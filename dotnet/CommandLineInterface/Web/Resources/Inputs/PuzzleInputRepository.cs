namespace AdventOfCode.Console.Web
{
    public class PuzzleInputRepository : IPuzzleInputRepository
    {
        private static IPuzzleInputRepository? instance;
        private readonly IAdventOfCodeClient _client = AdventOfCodeClient.Instance;

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

        protected PuzzleInputRepository() : this(AdventOfCodeClient.Instance)
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
                throw new InvalidOperationException(GetNotFoundErrorMessage(year, day), e);
            }

            return lines.ToArray();
        }
    }
}
