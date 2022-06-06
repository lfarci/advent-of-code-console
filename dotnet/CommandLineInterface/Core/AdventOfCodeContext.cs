using AdventOfCode.Console.Web.Resources;

namespace AdventOfCode.Console.Core
{
    public class AdventOfCodeContext
    {

        private static ICalendarPageRepository calendarPageRepository = CalendarPageRepository.Instance;
        private static IDayPageRepository dayPageRepository = DayPageRepository.Instance;

        private IEnumerable<Day> _days;
        public int Year { get; }

        public AdventOfCodeContext(int year)
        {
            _days = new List<Day>();
            Year = year;
        }

        private bool IsActivatorException(Exception e)
        {
            return e is ApplicationException || e is SystemException;
        }

        private async Task<Day> From(CalendarPage.DayEntry day)
        {
            var dayPage = await dayPageRepository.FindByYearAndDayAsync(Year, day.Index);
            return new Day
            {
                Index = day.Index,
                Completion = day.Completion,
                FirstPuzzleAnswer = dayPage.FirstPuzzleAnswer,
                SecondPuzzleAnswer = dayPage.SecondPuzzleAnswer,
                Puzzle = null
            };
        }

        public async Task Initialize(Action<AdventOfCodeContext> onInitialized)
        {
            var calendar = await calendarPageRepository.FindByYearAsync(Year);
            _days = await Task.WhenAll(calendar.Days.Select(async d => await From(d)));
            onInitialized(this);
        }

        private void RegisterPuzzleForDay(int index, Puzzle puzzle)
        {
            Day? day = _days.Where(d => d.Index == index).ToList().FirstOrDefault();
            if (day == null)
            {
                throw new ArgumentOutOfRangeException($"No day {index} for year {Year}.");
            }
            day.Puzzle = puzzle;
        }

        public class PuzzleSubmission
        {
            public Action<int>? RegisterForDay { get; set; }

            public void Run(int dayIndex)
            {
                RegisterForDay?.Invoke(dayIndex);
            }
        }

        public PuzzleSubmission Submit<TPuzzle>() where TPuzzle : Puzzle
        {
            try
            {
                Type type = typeof(TPuzzle);
                TPuzzle? puzzle = (TPuzzle?)Activator.CreateInstance(type);
                if (puzzle == null)
                {
                    throw new InvalidOperationException("Failed to create an instance of the submitted puzzle class.");
                }
                return new PuzzleSubmission
                {
                    RegisterForDay = index => RegisterPuzzleForDay(index, puzzle)
                };
            }
            catch (Exception e) when (IsActivatorException(e))
            {
                throw new InvalidOperationException("Could not construct the puzzle.", e);
            }
        }
    }
}
