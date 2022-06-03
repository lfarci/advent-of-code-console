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

        public async virtual Task<Stream> FindCalendarPageAsStreamAsync(int year)
        {
            return await client.GetCalendarPageAsStreamAsync(year);
        }

        public async Task<CalendarPage> FindByYearAsync(int year)
        {
            try
            {
                using Stream stream = await FindCalendarPageAsStreamAsync(year);
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
