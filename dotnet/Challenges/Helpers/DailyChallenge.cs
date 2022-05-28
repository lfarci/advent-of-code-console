namespace AdventOfCode2021.Helpers
{
    interface IDailyChallenge
    {
        void RegisterResultEntry(string resultEntry);
        Task ShowResults();
    }

    public abstract class DailyChallenge : IDailyChallenge
    {

        public string Title { get; }
        public int Year { get { return 2021; } }
        public int Day { get; }

        private List<string> _resultEntries;

        public DailyChallenge(string title, int day)
        {
            Title = title;
            Day = day;
            _resultEntries = new List<string>();
        }

        protected async Task<string[]> ReadAllInputLines()
        {
            return await DailyChallengeInputResolver.ReadAllLinesFrom(Year, Day);
        }

        public void RegisterResultEntry(string resultEntry)
        {
            _resultEntries.Add(resultEntry);
        }

        protected abstract void Run(string[] lines);

        public async Task ShowResults()
        {
            string[] lines = await ReadAllInputLines();
            Run(lines);
            Console.Write($"Day {Day} - {Title}:");
            if (_resultEntries.Count == 0)
            {
                Console.Write(" no results.");
            }
            else
            {
                Console.Write("\n");
                for (int i = 0; i < _resultEntries.Count; i++)
                {
                    Console.WriteLine($"- Result {i + 1}: {_resultEntries[i]}.");
                }
            }
        }
    }
}
