using AdventOfCode.Kit.Client.Web.Resources;

namespace AdventOfCode.Kit.Client.Web
{
    internal class AdventOfCodeWebsite : IResourceRepository
    {
        internal ICalendarPageRepository CalendarPages { get; init; }
        internal IDayPageRepository DayPages { get; init; }
        internal IPuzzleInputRepository PuzzleInputs { get; init; }

        public AdventOfCodeWebsite(IClientConfiguration configuration)
        {
            CalendarPages = new CalendarPageRepository(configuration);
            DayPages = new DayPageRepository(configuration);
            PuzzleInputs = new PuzzleInputRepository(configuration);
        }

        internal AdventOfCodeWebsite(
            ICalendarPageRepository calendarPages,
            IDayPageRepository dayPages, 
            IPuzzleInputRepository puzzleInputs)
        {
            CalendarPages = calendarPages;
            DayPages = dayPages;
            PuzzleInputs = puzzleInputs;
        }

        public async Task<CalendarPage> FindCalendarPageByYearAsync(int year)
        {
            return await CalendarPages.FindByYearAsync(year);
        }

        public Task<DayPage> FindDayPageByYearAndDayAsync(int year, int day)
        {
            return DayPages.FindByYearAndDayAsync(year, day);
        }

        public Task<string[]> FindPuzzleInputByYearAndDayAsync(int year, int day)
        {
            return PuzzleInputs.FindByYearAndDayAsync(year, day);
        }
    }
}
