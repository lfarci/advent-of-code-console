using AdventOfCode2021.Challenges;

namespace AdventOfCode2021.Helpers
{
    class DailyChallengeResolver
    {
        private static IDictionary<string, DailyChallenge> challenges = new Dictionary<string, DailyChallenge>
        {
            { "01", new SonarSweepChallenge() },
            { "02", new DiveChallenge() },
            { "03", new BinaryDiagnosticChallenge() }
        };

        public static DailyChallenge? Resolve(string challengeKey)
        {
            if (challenges.ContainsKey(challengeKey))
                return challenges[challengeKey];
            return null;
        }

        public static string[] FindAllChallengeKeys()
        {
            return challenges.Keys.ToArray();
        }

    }
}