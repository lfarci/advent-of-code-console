using AdventOfCode2021.CommandLineInterface.Client;

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
            var calendar = new AdventOfCodeCalendar();
            try
            {
                using Stream stream = await client.GetCalendarPageAsStreamAsync(year);
                using StreamReader reader = new(stream);

                string s = await reader.ReadToEndAsync();

                Console.WriteLine(s);
                AdventOfCodeCalendar.Parse(s);
            }
            catch (AdventOfCodeClientException e)
            {
                throw new ArgumentOutOfRangeException($"Could not find cannot calendar for year {year}.", e);
            }
            return calendar;
        }
    }
}
