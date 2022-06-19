using AdventOfCode.Console.Web;

namespace AdventOfCode.Console.Core
{
    public class Day
    {
        public int Index { get; set; }
        public string Title { get; set; } = "";
        public Completion Completion { get; set; }
        public long? FirstPuzzleAnswer { get; set; } = null;
        public long? SecondPuzzleAnswer { get; set; } = null;
        internal Puzzle? Puzzle { get; set; }
    }
}
