using AdventOfCode.Console.Web;
using static AdventOfCode.Console.Web.CalendarPage;

namespace AdventOfCode.Console.Core
{
    public class AdventOfCodeWebsite : IDataSource
    {
        private static ICalendarPageRepository calendarPageRepository = CalendarPageRepository.Instance;
        private static IDayPageRepository dayPageRepository = DayPageRepository.Instance;

        private async Task<Day> GetCalendarDayAsync(int year, DayEntry calendarPageDayEntry)
        {
            var dayPage = await dayPageRepository.FindByYearAndDayAsync(year, calendarPageDayEntry.Index);
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
            CalendarPage page = await calendarPageRepository.FindByYearAsync(year);
            Calendar calendar = CalendarPageMapper.ToCalendar(page);
            calendar.Days = await GetCalendarDaysAsync(page);
            return calendar;
        }
    }
}
