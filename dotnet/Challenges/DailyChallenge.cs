namespace AdventOfCode2021.Challenges
{
    public abstract class DailyChallenge
    {
        public class PuzzleAnswer
        {
            public string Description { get; }
            public long Value { get; }

            public PuzzleAnswer(long value, string description)
            {
                Description = description;
                Value = value;
            }
        }
        
        public static int Year { get { return 2021;  } }
        public string Title { get; }
        public int Day { get; }

        protected DailyChallenge(string title, int day)
        {
            Title = title;
            Day = day;
        }

        public abstract IEnumerable<PuzzleAnswer> Run(string[] lines);
    }
}
