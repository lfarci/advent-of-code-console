using AdventOfCode2021.Challenges;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public interface IChallengeRepository
    {
        List<DailyChallenge> Challenges { get; }

        DailyChallenge? FindByDay(int day);

        DailyChallenge? FindByDay(string day);
    }
}
