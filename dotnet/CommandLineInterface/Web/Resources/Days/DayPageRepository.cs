namespace AdventOfCode.CommandLineInterface.Web
{
    public class DayPageRepository : IDayPageRepository
    {
        private static IDayPageRepository? instance;
        private readonly IAdventOfCodeClient _client;
        public static IDayPageRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DayPageRepository();
                }
                return instance;
            }
        }

        protected DayPageRepository() : this(AdventOfCodeClient.Instance)
        { }

        public DayPageRepository(IAdventOfCodeClient client)
        {
            _client = client;
        }

        public static string GetParseErrorMessage(int year, int day)
        {
            return $"Could not parse response for year {year} and day {day}.";
        }

        public static string GetNotFoundErrorMessage(int year, int day)
        {
            return $"Could not find page for year {year} and day {day}.";
        }

        public async Task<DayPage> FindByYearAndDayAsync(int year, int day)
        {
            try
            {
                using Stream stream = await _client.GetDayPageAsStreamAsync(year, day);
                using StreamReader reader = new(stream);
                return DayPage.Parse(await reader.ReadToEndAsync());
            }
            catch (FormatException e)
            {
                throw new InvalidOperationException(GetParseErrorMessage(year, day), e);
            }
            catch (IOException e)
            {
                throw new IOException(GetNotFoundErrorMessage(year, day), e);
            }
        }
    }
}
