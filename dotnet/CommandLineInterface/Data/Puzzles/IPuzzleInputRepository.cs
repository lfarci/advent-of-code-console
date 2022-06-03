using AdventOfCode2021.CommandLineInterface.WebClient;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public interface IPuzzleInputRepository
    {
        public Task<string[]> FindByYearAndDayAsync(int year, int day);
    }
}
