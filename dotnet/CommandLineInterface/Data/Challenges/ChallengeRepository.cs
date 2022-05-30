using AdventOfCode2021.Challenges;

namespace AdventOfCode2021.CommandLineInterface.Data
{
    public class ChallengeRepository : IChallengeRepository
    {
        private static IChallengeRepository? instance;
        private static List<DailyChallenge> challenges = new()
        {
            new SonarSweepChallenge(1, "Sonar Sweep"),
            new DiveChallenge(2, "Dive!"),
            new BinaryDiagnosticChallenge(3, "Binary Diagnostic")
        };

        protected ChallengeRepository()
        {
        }

        public static IChallengeRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChallengeRepository();
                }
                return instance;
            }
        }

        public List<DailyChallenge> Challenges { get => challenges; }

        public DailyChallenge? FindByDay(int day) => challenges.FirstOrDefault(c => c.Day == day);

        public DailyChallenge? FindByDay(string day)
        {
            DailyChallenge? challenge;
            try
            {
                int result = int.Parse(day);
                challenge = FindByDay(result);
            }
            catch (FormatException)
            {
                challenge = null;
            }
            return challenge;
        }
    }
}