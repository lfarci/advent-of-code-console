using AdventOfCode.Kit.Client.Web.Resources;

namespace AdventOfCode.Kit.Client.Web
{
    internal class AdventOfCodeWebsite : IResourceRepository
    {
        private static IResourceRepository? instance;

        internal ICalendarPageRepository CalendarPages { get; init; } = CalendarPageRepository.Instance;
        internal IDayPageRepository DayPages { get; init; } = DayPageRepository.Instance;
        internal IPuzzleInputRepository PuzzleInputs { get; init; } = PuzzleInputRepository.Instance;

        public static IResourceRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdventOfCodeWebsite();
                }
                return instance;
            }
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
