using AdventOfCode.Console.Web.Resources;

namespace AdventOfCode.Console.Web
{
    internal class ResourceRepository : IResourceRepository
    {
        private static IResourceRepository? instance;

        public ICalendarPageRepository CalendarPages { get; init; } = CalendarPageRepository.Instance;
        public IDayPageRepository DayPages { get; init; } = DayPageRepository.Instance;
        public IPuzzleInputRepository PuzzleInputs { get; init; } = PuzzleInputRepository.Instance;

        public static IResourceRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceRepository();
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
