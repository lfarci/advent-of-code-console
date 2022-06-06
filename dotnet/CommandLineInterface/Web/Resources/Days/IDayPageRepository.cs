namespace AdventOfCode.Console.Web
{
    public interface IDayPageRepository
    {
        public Task<DayPage> FindByYearAndDayAsync(int year, int day);
    }
}
