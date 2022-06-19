namespace AdventOfCode.Console.Core
{
    public interface IPuzzleSubmitter
    {
        public int Year { get; }
        public IPuzzleSubmitter Submit<TPuzzle>() where TPuzzle : Puzzle, new();
        public void ForDay(int dayIndex);
    }
}
