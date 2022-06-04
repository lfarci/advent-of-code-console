using AdventOfCode.CommandLineInterface.Web;

namespace AdventOfCode.CommandLineInterface.Core
{
    public class Day
    {
        public int Index { get; set; }
        public Completion Completion { get; set; }
        public long? FirstPuzzleAnswer { get; set; } = null;
        public long? SecondPuzzleAnswer { get; set; } = null;
        public Puzzle? Puzzle { get; set; }
    }
}
