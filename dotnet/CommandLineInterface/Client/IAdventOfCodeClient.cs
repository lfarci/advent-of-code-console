namespace AdventOfCode2021.CommandLineInterface.Client
{
    public interface IAdventOfCodeClient
    {
        Task<Stream> GetCalendarPageAsStreamAsync(int year);
        Task<Stream> GetDailyChallengeInputAsStreamAsync(int year, int day);
    }
}