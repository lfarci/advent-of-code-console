using AdventOfCode.Kit.Client.Models;
using AdventOfCode.Kit.Client.Web;
using AdventOfCode.Kit.Client.Web.Resources;
using static AdventOfCode.Kit.Client.Web.Resources.CalendarPage;

namespace AdventOfCode.Kit.Client
{
    public class AdventOfCodeClient : IAdventOfCode
    {
        internal IResourceRepository Resources { get; init; }

        public AdventOfCodeClient() 
        {
            var configuration = new AdventOfCodeWebsiteConfiguration();
            Resources = new AdventOfCodeWebsite(configuration);
        }

        internal AdventOfCodeClient(IResourceRepository resources)
        {
            Resources = resources;
        }

        private async Task<Day> GetCalendarDayAsync(int year, DayEntry calendarPageDayEntry)
        {
            var dayPage = await Resources.FindDayPageByYearAndDayAsync(year, calendarPageDayEntry.Index);
            return new DayBuilder()
                .WithTitle(dayPage.Title)
                .WithIndex(calendarPageDayEntry.Index)
                .WithCompletion(dayPage.Completion)
                .WithFirstPuzzleAnswer(dayPage.FirstPuzzleAnswer)
                .WithSecondPuzzleAnswer(dayPage.SecondPuzzleAnswer)
                .Build();
        }

        private async Task<IEnumerable<Day>> GetCalendarDaysAsync(CalendarPage page)
        {
            var tasks = page.Days.Select(async d => await GetCalendarDayAsync(page.Year, d));
            var days = await Task.WhenAll(tasks);
            return days;
        }

        public async Task<Calendar> FindCalendarByYearAsync(int year)
        {
            CalendarPage page = await Resources.FindCalendarPageByYearAsync(year);
            Calendar calendar = CalendarPageMapper.ToCalendar(page);
            calendar.Days = await GetCalendarDaysAsync(page);
            return calendar;
        }

        public async Task<string[]> FindPuzzleInputByYearAndDayAsync(int year, int day)
        {
            return await Resources.FindPuzzleInputByYearAndDayAsync(year, day);
        }
    }
}
