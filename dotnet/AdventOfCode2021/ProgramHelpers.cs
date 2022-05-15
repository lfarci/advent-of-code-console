using AdventOfCode2021.Challenges;
using System.Reflection;

namespace AdventOfCode2021.Helpers
{

    interface IDailyChallenge
    {
        void Run();
    }

    class DailyChallengeResolver
    {
        private static IDictionary<string, IDailyChallenge> challenges = new Dictionary<string, IDailyChallenge>
        {
            { "01", new SonarSweepChallenge() }
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

    public class ChallengeResourceManager
    {

        static Stream GetResourceAsStream(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Stream? stream = assembly.GetManifestResourceStream(resourceName);

            string[] names = assembly.GetManifestResourceNames();
            if (stream == null)
            {
                throw new IOException($"Could not open a stream for a resource: {resourceName}.");
            }
            else
            {
                return stream;
            }
        }

        public static string[] ReadAllLinesFrom(string resourceName)
        {
            List<string> lines = new List<string>();
            using (Stream stream = GetResourceAsStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines.ToArray();
        }

    }
}
