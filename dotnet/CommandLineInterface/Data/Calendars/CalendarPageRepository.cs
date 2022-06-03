using AdventOfCode2021.CommandLineInterface.WebClient;

namespace CommandLineInterface.Data
{
    public class CalendarPageRepository : ICalendarPageRepository
    {
        private static ICalendarPageRepository? instance;
        private readonly IAdventOfCodeClient _client = AdventOfCodeClient.Instance;
        public static ICalendarPageRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CalendarPageRepository();
                }
                return instance;
            }
        }

        protected CalendarPageRepository() : this(AdventOfCodeClient.Instance)
        { }

        public CalendarPageRepository(IAdventOfCodeClient client)
        {
            _client = client;
        }

        public async Task<CalendarPage> FindByYearAsync(int year)
        {
            try
            {
                using Stream stream = await _client.GetCalendarPageAsStreamAsync(year);
                using StreamReader reader = new(stream);
                return CalendarPage.Parse(await reader.ReadToEndAsync());
            }
            catch (FormatException e)
            {
                throw new InvalidOperationException($"Could not parse calendar for year {year}.", e);
            }
            catch (IOException e)
            {
                throw new IOException($"Could not find cannot calendar for year {year}.", e);
            }
        }
    }
}
