using AdventOfCode.Kit.Client;
using AdventOfCode.Kit.Client.Models;

namespace AdventOfCode.Kit.Console.Core
{
    public class AdventOfCodeContext : IPuzzleSubmitter
    {
        internal readonly static string AlreadyInitializedErrorMessage = "This context is already initialized.";
        internal readonly static string NotInitializedErrorMessage = "This context should be initialized.";
        internal readonly static string PuzzleCreationFailureErrorMessage = "Could not construct the puzzle.";
        internal readonly static string NoSubmittedPuzzlesErrorMessage = "No puzzle to submit.";

        private readonly IAdventOfCodeClient _data;
        private Calendar? _calendar;
        private Queue<Puzzle> _submittedPuzzles;

        internal bool Initialized { get => _calendar != null; }
        public Calendar? Calendar { get => _calendar; }
        public int Year { get; }
        internal bool HasSubmittedPuzzles { get => _submittedPuzzles.Count > 0; }

        internal AdventOfCodeContext(int year, IAdventOfCodeClient client)
            : this(
                  client,
                  null,
                  year
              )
        { }

        internal AdventOfCodeContext(IAdventOfCodeClient data, Calendar? calendar, int year)
        {
            _data = data;
            _calendar = calendar;
            _submittedPuzzles = new Queue<Puzzle>();
            Year = year;
        }

        internal async Task Initialize(Action<IPuzzleSubmitter> onInitialized)
        {
            if (Initialized)
            {
                throw new InvalidOperationException(AlreadyInitializedErrorMessage);
            }
            _calendar = await _data.FindCalendarByYearAsync(Year);
            onInitialized(this);
        }

        public IPuzzleSubmitter Submit<TPuzzle>() where TPuzzle : Puzzle, new()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException(NotInitializedErrorMessage);
            }
            _submittedPuzzles.Enqueue(new TPuzzle());
            return this;
        }

        public void ForDay(int index)
        {
            if (_calendar == null)
            {
                throw new InvalidOperationException(NotInitializedErrorMessage);
            }
            if (!HasSubmittedPuzzles)
            {
                throw new InvalidOperationException(NoSubmittedPuzzlesErrorMessage);
            }
            _calendar[index].Puzzle = _submittedPuzzles.Dequeue();
        }
    }
}
