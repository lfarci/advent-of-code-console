using AdventOfCode.CommandLineInterface.Web;

namespace AdventOfCode.CommandLineInterface.Core
{
    public class AdventOfCodeContext
    {

        private static ICalendarPageRepository calendarPageRepository = CalendarPageRepository.Instance;
        private static IDayPageRepository dayPageRepository = DayPageRepository.Instance;

        private IList<Day> _days;
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

        public class RegisterCallback
        {
            public Action<int>? RegisterForDay { get; set; }

            public void ForDay(int dayIndex)
            {
                RegisterForDay?.Invoke(dayIndex);
            }
        }

        private async Task<Day> From(CalendarPage.DayEntry day)
        {
            var dayPage = await dayPageRepository.FindByYearAndDayAsync(Year, day.Index);
            return new Day {
                Index = day.Index,
                Completion = day.Completion,
                FirstPuzzleAnswer = dayPage.FirstPuzzleAnswer,
                SecondPuzzleAnswer = dayPage.SecondPuzzleAnswer,
                Puzzle = null
            };
        }

        public async Task Initialize(Action<AdventOfCodeContext> onInitialized)
        {
            Console.WriteLine("Starting to initialize");

            try
            {
                var calendar = await calendarPageRepository.FindByYearAsync(Year);
                var days = await Task.WhenAll(calendar.Days.Select(async d => await From(d)));

                _days = new List<Day>(days);
                Console.WriteLine("Initialized");
                onInitialized(this);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"Because: {e.InnerException?.Message}");
                Console.WriteLine($"Because: {e.InnerException?.InnerException?.Message}");
            }

        }

        private void RegisterPuzzle(int day, Puzzle puzzle)
        { 
        
        }

        public RegisterCallback Submit<TPuzzle>() where TPuzzle : Puzzle
        {
            try
            {
                Type type = typeof(TPuzzle);
                TPuzzle? puzzle = (TPuzzle?)Activator.CreateInstance(type);
                if (puzzle == null)
                {
                    throw new InvalidOperationException();
                }

                // I should have the calendar in the memory and find the day corresponding
                // the given year and day, the set the day puzzle.

                //_puzzleRepository.Save(puzzle);
                return new RegisterCallback { RegisterForDay = day => RegisterPuzzle(day, puzzle) };
            }
            catch (Exception e) when (IsActivatorException(e))
            {
                throw new InvalidOperationException("Could not construct the puzzle.", e);
            }
        }
    }
}
