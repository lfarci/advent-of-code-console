using AdventOfCode.Kit.Client.Web.Http;

namespace AdventOfCode.Kit.Client.Web.Resources
{
    internal class DayPageRepository : IDayPageRepository
    {
        private readonly Http.IAdventOfCodeClient _client;

        public DayPageRepository(IClientConfiguration configuration)
            : this(new AdventOfCodeHttpClient(configuration))
        { }

        public DayPageRepository(Http.IAdventOfCodeClient client)
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

        public async Task<DayPage> FindByYearAndDayAsync(int year, int index)
        {
            try
            {
                using Stream stream = await _client.GetDayPageAsStreamAsync(year, index);
                using StreamReader reader = new(stream);
                var day = DayPage.Parse(await reader.ReadToEndAsync());
                day.Index = index;
                return day;
            }
            catch (FormatException e)
            {
                throw new InvalidOperationException(GetParseErrorMessage(year, index), e);
            }
            catch (IOException e)
            {
                throw new IOException(GetNotFoundErrorMessage(year, index), e);
            }
        }
    }
}
