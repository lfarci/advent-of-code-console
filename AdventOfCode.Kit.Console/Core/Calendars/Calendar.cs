namespace AdventOfCode.Kit.Console.Core
{
    public class Calendar
    {
        public int Year { get; set; }
        internal int Length { get; set; } = 25;
        internal IEnumerable<Day> Days { get; set; } = new List<Day>();

        internal Day this[int index]
        {
            get => FindDayByIndex(index);
            set => this[index] = value;
        }

        internal void SubmitPuzzleForDay(int index, Puzzle puzzle)
        {
            this[index].Puzzle = puzzle;
        }

        private Day FindDayByIndex(int index)
        {
            Day? result = Days.FirstOrDefault(d => d.Index == index);
            if (result == null)
            {
                throw new ArgumentOutOfRangeException($"Index out of range: {index}");
            }
            return result;
        }
    }
}
