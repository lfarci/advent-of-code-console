using AdventOfCode.Kit.Client;
using AdventOfCode.Kit.Client.Resources;
using static AdventOfCode.Kit.Client.Resources.CalendarPage;

namespace AdventOfCode.Kit.Client.Core
{
    public class AdventOfCodeWebsite : IDataSource
    {
        internal IResourceRepository Resources { get; init; } = ResourceRepository.Instance;

        public AdventOfCodeWebsite() { 
            Resources = ResourceRepository.Instance;
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
