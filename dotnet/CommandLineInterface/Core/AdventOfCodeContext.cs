using AdventOfCode.Console.Web;

namespace AdventOfCode.Console.Core
{
    public class AdventOfCodeContext
    {

        private static IDataSource data = new AdventOfCodeWebsite { 
            Resources = ResourceRepository.Instance
        };

        private Calendar _calendar;
        public int Year { get; }

        public AdventOfCodeContext(int year)
        {
            _calendar = new Calendar();
            Year = year;
        }

        private bool IsActivatorException(Exception e)
        {
            return e is ApplicationException || e is SystemException;
        }

        public async Task Initialize(Action<AdventOfCodeContext> onInitialized)
        {
            _calendar = await data.FindCalendarByYearAsync(Year);
            onInitialized(this);
        }

        private void RegisterPuzzleForDay(int index, Puzzle puzzle)
        {
          _calendar[index].Puzzle = puzzle;
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
