namespace AdventOfCode.CommandLineInterface.Web
{
    public interface IPuzzleInputRepository
    {
        public Task<string[]> FindByYearAndDayAsync(int year, int day);
    }
}
