using AdventOfCode.Kit.Client.Web.Http;

namespace AdventOfCode.Kit.Client.Web.Resources
{
    internal class CalendarPageRepository : ICalendarPageRepository
    {
        private readonly IAdventOfCodeClient _client;

        internal CalendarPageRepository(IConfiguration configuration)
            : this(new AdventOfCodeHttpClient(configuration))
        { }

        public CalendarPageRepository(IAdventOfCodeClient client)
        {
            _client = client;
        }

        public static string GetParseErrorMessage(int year) => $"Could not parse calendar for year {year}.";
        public static string GetNotFoundErrorMessage(int year) => $"Could not find cannot calendar for year {year}.";

        public async Task<CalendarPage> FindByYearAsync(int year)
        {
            try
            {
                using Stream stream = await _client.GetCalendarPageAsStreamAsync(year);
                using StreamReader reader = new(stream);
                var calendar = CalendarPage.Parse(await reader.ReadToEndAsync());
                calendar.Year = year;
                return calendar;
            }
            catch (FormatException e)
            {
                throw new InvalidOperationException(GetParseErrorMessage(year), e);
            }
            catch (IOException e)
            {
                throw new IOException(GetNotFoundErrorMessage(year), e);
            }
        }
    }
}
