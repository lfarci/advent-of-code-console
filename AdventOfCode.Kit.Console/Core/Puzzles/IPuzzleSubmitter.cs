namespace AdventOfCode.Kit.Console.Core
{
    public interface IPuzzleSubmitter
    {
        public int Year { get; }
        public Calendar? Calendar { get; }
        public IPuzzleSubmitter Submit<TPuzzle>() where TPuzzle : Puzzle, new();
        public void ForDay(int dayIndex);
    }
}
