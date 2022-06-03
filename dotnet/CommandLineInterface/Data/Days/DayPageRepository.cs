using AdventOfCode2021.CommandLineInterface.WebClient;

namespace CommandLineInterface.Data
{
    public class DayPageRepository : IDayPageRepository
    {
        private static IDayPageRepository? instance;
        private static readonly IAdventOfCodeClient client = AdventOfCodeClient.Instance;
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

        public async Task<DayPage> FindByYearAndDayAsync(int year, int day)
        {
            try
            {
                using Stream stream = await client.GetDayPageAsStreamAsync(year, day);
                using StreamReader reader = new(stream);
                return DayPage.Parse(await reader.ReadToEndAsync());
            }
            catch (FormatException e)
            {
                throw new ArgumentOutOfRangeException($"Could not parse response for year {year} and day {day}.", e);
            }
            catch (IOException e)
            {
                throw new ArgumentOutOfRangeException($"Could not find page for year {year} and day {day}.", e);
            }
        }
    }
}
