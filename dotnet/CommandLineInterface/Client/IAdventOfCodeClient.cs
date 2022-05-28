namespace AdventOfCode2021.CommandLineInterface.Client
{
    internal interface IAdventOfCodeClient
    {
        Task<Stream> GetDailyChallengeInputAsStreamAsync(int year, int day);
    }
}