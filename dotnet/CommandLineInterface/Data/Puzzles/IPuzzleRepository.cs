using AdventOfCode2021.Challenges;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public interface IPuzzleRepository
    {
        List<DailyChallenge> Challenges { get; }

        DailyChallenge? FindByDay(int day);

        DailyChallenge? FindByDay(string day);
    }
}
