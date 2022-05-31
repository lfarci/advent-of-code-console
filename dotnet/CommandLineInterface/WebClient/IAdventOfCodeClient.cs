namespace AdventOfCode2021.CommandLineInterface.WebClient
{
    public interface IAdventOfCodeClient
    {
        Task<Stream> GetCalendarPageAsStreamAsync(int year);
        Task<Stream> GetDayPageAsStreamAsync(int year, int day);
        Task<Stream> GetPuzzleInputAsStreamAsync(int year, int day);
    }
}