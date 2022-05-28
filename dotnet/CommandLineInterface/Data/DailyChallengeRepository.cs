using AdventOfCode2021.Challenges;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    class DailyChallengeRepository
    {
        private static List<DailyChallenge> challenges = new List<DailyChallenge>
        {
            new SonarSweepChallenge(1, "Sonar Sweep"),
            new DiveChallenge(2, "Dive!"),
            new BinaryDiagnosticChallenge(3, "Binary Diagnostic")
        };

        public static DailyChallenge? FindByDay(int day) => challenges.FirstOrDefault(c => c.Day == day);

        public static DailyChallenge? FindByDay(string day)
        {
            DailyChallenge? challenge;
            try
            {
                int result = Int32.Parse(day);
                challenge = FindByDay(result);
            }
            catch (FormatException)
            {
                challenge = null;
            }
            return challenge;
        }

        public static string[] FindAllChallengeKeys()
        {
            return new string[0];
        }

    }
}