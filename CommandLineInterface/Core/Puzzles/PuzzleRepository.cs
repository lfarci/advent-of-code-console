namespace AdventOfCode.Console.Core
{
    public class PuzzleRepository : IPuzzleRepository
    {
        private static IPuzzleRepository? instance;
        private readonly List<Puzzle> _puzzles;

        protected PuzzleRepository()
        {
            this._puzzles = new List<Puzzle>();
        }

        public static IPuzzleRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PuzzleRepository();
                }
                return instance;
            }
        }

        public List<Puzzle> Puzzles { get => _puzzles; }

        public Puzzle? FindByDay(int day) => null;

        public Puzzle? FindByDay(string day)
        {
            Puzzle? puzzle;
            try
            {
                int result = int.Parse(day);
                puzzle = FindByDay(result);
            }
            catch (FormatException)
            {
                puzzle = null;
            }
            return puzzle;
        }

        public void Save(Puzzle puzzle)
        { 
            _puzzles.Add(puzzle);
        }
    }
}