namespace AdventOfCode.CommandLineInterface.Core
{
    public interface IPuzzleRepository
    {
        List<Puzzle> Puzzles { get; }

        Puzzle? FindByDay(int day);

        Puzzle? FindByDay(string day);

        void Save(Puzzle puzzle);
    }
}
