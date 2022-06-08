using AdventOfCode.Console.Web;

namespace AdventOfCode.Console.Core
{
    public class AdventOfCodeContext
    {
        public class PuzzleSubmission
        {
            internal Action<int>? Register { get; init; }

            public void ForDay(int dayIndex)
            {
                Register?.Invoke(dayIndex);
            }
        }

        internal readonly static string AlreadyInitializedErrorMessage = "This context is already initialized.";
        internal readonly static string NotInitializedErrorMessage = "This context should be initialized.";
        internal readonly static string PuzzleCreationFailureErrorMessage = "Could not construct the puzzle.";

        private readonly IDataSource _data;
        private Calendar? _calendar;

        internal bool Initialized { get => _calendar != null; }
        internal Calendar? Calendar { get => _calendar; }
        internal int Year { get; }

        internal AdventOfCodeContext(int year)
            : this(
                  new AdventOfCodeWebsite { Resources = ResourceRepository.Instance },
                  null,
                  year
              )
        { }

        internal AdventOfCodeContext(IDataSource data, Calendar? calendar, int year)
        {
            _data = data;
            _calendar = calendar;
            Year = year;
        }

        internal async Task Initialize(Action<AdventOfCodeContext> onInitialized)
        {
            if (Initialized)
            {
                throw new InvalidOperationException(AlreadyInitializedErrorMessage);
            }
            _calendar = await _data.FindCalendarByYearAsync(Year);
            onInitialized(this);
        }

        internal void RegisterPuzzleForDay(int index, Puzzle puzzle)
        {
            if (_calendar == null)
            {
                throw new InvalidOperationException(NotInitializedErrorMessage);
            }
            _calendar[index].Puzzle = puzzle;
        }

        public PuzzleSubmission Submit<TPuzzle>() where TPuzzle : Puzzle, new()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException(NotInitializedErrorMessage);
            }
            return new PuzzleSubmission
            {
                Register = index => RegisterPuzzleForDay(index, new TPuzzle())
            };

        }
    }
}
