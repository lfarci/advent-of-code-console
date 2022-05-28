namespace AdventOfCode2021.CommandLineInterface.Client
{
    public interface IAdventOfCodeClient
    {
        Task<Stream> GetDailyChallengeInputAsStreamAsync(int year, int day);
    }
}