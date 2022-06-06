namespace AdventOfCode.Console.Core
{
    public class Calendar
    {
        public int Year { get; set; }
        public int Length { get; set; } = 25;
        public IEnumerable<Day> Days { get; set; } = new List<Day>();
    }
}
