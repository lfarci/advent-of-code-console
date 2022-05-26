using AdventOfCode2021.Challenges;

namespace AdventOfCode2021.Helpers
{
    internal class DailyChallengeResolver
    {
        private static IDictionary<string, IDailyChallenge> challenges = new Dictionary<string, IDailyChallenge>
        {
            { "01", new SonarSweepChallenge() },
            { "02", new DiveChallenge() }
        };

        public static IDailyChallenge? Resolve(string challengeKey)
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
