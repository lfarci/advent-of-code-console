using AdventOfCode.Console.Web;
using static AdventOfCode.Console.Web.CalendarPage;

namespace AdventOfCode.Console.Core
{
    public class AdventOfCodeWebsite : IDataSource
    {
        private static ICalendarPageRepository calendarPageRepository = CalendarPageRepository.Instance;
        private static IDayPageRepository dayPageRepository = DayPageRepository.Instance;

        private async Task<Day> GetCalendarDayAsync(int year, DayEntry calendarPageEntry)
        {
            var dayPage = await dayPageRepository.FindByYearAndDayAsync(year, calendarPageEntry.Index);
            return new Day
            {
                Index = calendarPageEntry.Index,
                Completion = calendarPageEntry.Completion,
                FirstPuzzleAnswer = dayPage.FirstPuzzleAnswer,
                SecondPuzzleAnswer = dayPage.SecondPuzzleAnswer,
                Puzzle = null
            };
        }

        private async Task<IEnumerable<Day>> GetCalendarDaysAsync(CalendarPage page)
        {
            var tasks = page.Days.Select(async d => await GetCalendarDayAsync(0, d));
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
