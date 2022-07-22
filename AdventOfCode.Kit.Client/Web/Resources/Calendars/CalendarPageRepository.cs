using AdventOfCode.Kit.Client.Web.Http;

namespace AdventOfCode.Kit.Client.Resources
{
    internal class CalendarPageRepository : ICalendarPageRepository
    {
        private static ICalendarPageRepository? instance;
        private readonly IAdventOfCodeClient _client = AdventOfCodeHttpClient.Instance;
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

        protected CalendarPageRepository() : this(AdventOfCodeHttpClient.Instance)
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
