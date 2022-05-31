using AdventOfCode2021.CommandLineInterface.WebClient;

namespace CommandLineInterface.Data
{
    public class CalendarRepository : ICalendarRepository
    {
        private static ICalendarRepository? instance;
        private static readonly IAdventOfCodeClient client = AdventOfCodeClient.Instance;
        public static ICalendarRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CalendarRepository();
                }
                return instance;
            }
        }

        public async Task<AdventOfCodeCalendar> FindByYear(int year)
        {
            try
            {
                using Stream stream = await client.GetCalendarPageAsStreamAsync(year);
                using StreamReader reader = new(stream);
                return AdventOfCodeCalendar.Parse(await reader.ReadToEndAsync());
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
