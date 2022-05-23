using AdventOfCode2021.Helpers;

namespace AdventOfCode2021
{
    class Program
    {

        private static readonly string ProgramFriendlyName = AppDomain.CurrentDomain.FriendlyName;

        static async Task Main(string[] args)
        {
            if (args.Length == 1)
            {
                IDailyChallenge? challenge = DailyChallengeResolver.Resolve(args[0]);
                if (challenge != null)
                {
                    await challenge.ShowResults();
                }
                else
                {
                    string[] challengeKeys = DailyChallengeResolver.FindAllChallengeKeys();
                    string challengeKeysText = string.Join(",", challengeKeys);
                    Console.WriteLine($"No daily challenge could be resolved with key: \"{args[0]}\".");
                    Console.WriteLine($"Existing keys are: {challengeKeysText}");
                }
            }
            else
            {
                Console.WriteLine($"Usage: {ProgramFriendlyName} ## (Daily challenge key).");
            }
        }
    }
}