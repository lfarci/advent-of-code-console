using AdventOfCode2021.CommandLineInterface.WebClient;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public interface IChallengeInputRepository
    {
        public Task<Stream> FindInputStreamByYearAndDayAsync(int year, int day);
        public Task<string[]> FindInputLinesByYearAndDayAsync(int year, int day);
    }
}
