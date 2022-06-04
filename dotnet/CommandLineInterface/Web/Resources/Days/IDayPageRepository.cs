namespace AdventOfCode.CommandLineInterface.Web
{
    public interface IDayPageRepository
    {
        public Task<DayPage> FindByYearAndDayAsync(int year, int day);
    }
}
