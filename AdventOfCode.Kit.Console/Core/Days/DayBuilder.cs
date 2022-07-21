using AdventOfCode.Kit.Console.Web;

namespace AdventOfCode.Kit.Console.Core
{
    public class DayBuilder
    {
        private readonly Day _day = new();

        public DayBuilder WithIndex(int index)
        { 
            _day.Index = index;
            return this;
        }

        public DayBuilder WithTitle(string title)
        {
            _day.Title = title;
            return this;
        }

        public DayBuilder WithCompletion(Completion completion)
        {
            _day.Completion = completion;
            return this;
        }

        public DayBuilder WithFirstPuzzleAnswer(long? answer)
        {
            _day.FirstPuzzleAnswer = answer;
            return this;
        }

        public DayBuilder WithSecondPuzzleAnswer(long? answer)
        {
            _day.SecondPuzzleAnswer = answer;
            return this;
        }

        public Day Build() => _day;
    }
}
