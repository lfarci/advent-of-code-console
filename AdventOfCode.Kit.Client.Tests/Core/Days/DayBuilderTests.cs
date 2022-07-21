using AdventOfCode.Kit.Client.Core;
using AdventOfCode.Kit.Client.Resources;
using Xunit;

namespace AdventOfCode.Kit.Console.Tests.Core
{
    public class DayBuilderTests
    {
        private static readonly DayPage page = new DayPage { 
            Title = "My title",
            Completion = Completion.Complete,
            FirstPuzzleAnswer = 23423,
            SecondPuzzleAnswer = 23432
        };

        [Fact]
        public void WithIndex_ValidIndex_ReturnsDayWithIndex()
        {
            Assert.Equal(12, new DayBuilder().WithIndex(12).Build().Index);   
        }

        [Fact]
        public void WithTitle_ValidTitle_ReturnsDayWithTitle()
        {
            Assert.Equal("My title", new DayBuilder().WithTitle("My title").Build().Title);
        }

        [Fact]
        public void WithCompletion_ValidCompletion_ReturnsDayWithCompletion()
        {
            Assert.Equal(Completion.Complete, new DayBuilder().WithCompletion(Completion.Complete).Build().Completion);
        }

        [Fact]
        public void WithFirstPuzzleAnswer_ValidFirstPuzzleAnswer_ReturnsDayWithFirstPuzzleAnswer()
        {
            Assert.Equal(234234, new DayBuilder().WithFirstPuzzleAnswer(234234).Build().FirstPuzzleAnswer);
        }


        [Fact]
        public void WithSecondPuzzleAnswer_ValidSecondPuzzleAnswer_ReturnsDayWithSecondPuzzleAnswer()
        {
            Assert.Equal(234234, new DayBuilder().WithSecondPuzzleAnswer(234234).Build().SecondPuzzleAnswer);
        }
    }
}
