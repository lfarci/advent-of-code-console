using AdventOfCode2021.CommandLineInterface.WebClient;

namespace CommandLineInterface.Data
{
    public class CalendarPageRepository : ICalendarPageRepository
    {
        private static ICalendarPageRepository? instance;
        private static readonly IAdventOfCodeClient client = AdventOfCodeClient.Instance;
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

        public async Task<CalendarPage> FindByYear(int year)
        {
            try
            {
                using Stream stream = await client.GetCalendarPageAsStreamAsync(year);
                using StreamReader reader = new(stream);
                return CalendarPage.Parse(await reader.ReadToEndAsync());
            }
            catch (FormatException e)
            {
                throw new ArgumentOutOfRangeException($"Could not find cannot calendar for year {year}.", e);
            }
            catch (AdventOfCodeClientException e)
            {
                throw new ArgumentOutOfRangeException($"Could not find cannot calendar for year {year}.", e);
            }
        }
    }
}
