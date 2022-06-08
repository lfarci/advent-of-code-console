namespace AdventOfCode.Console.Core
{
    public abstract class Puzzle
    {
        public class Answer
        {
            public long Value { get; init; }
            public string Description { get; init; } = "";
        }

        public abstract (Answer First, Answer Second) Run(string[] lines);
    }
}
