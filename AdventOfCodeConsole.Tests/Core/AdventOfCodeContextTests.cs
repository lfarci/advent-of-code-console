using AdventOfCode.Console.Core;
using AdventOfCodeConsole.Tests.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Console.Tests.Core
{
    public class AdventOfCodeContextTests
    {

        class FixtureDataSource : IDataSource
        {
            public Task<Calendar> FindCalendarByYearAsync(int year)
            {
                return Task.FromResult(Fixtures.Calendar);
            }
        }

        [Fact]
        public void Constructor_OnlyValidYear_BuildsContextWithNullCalendar()
        {
            var context = new AdventOfCodeContext(2021);
            Assert.False(context.Initialized);
        }

        [Fact]
        public void Constructor_OnlyValidYear_BuildsContextWithGivenYear()
        {
            var context = new AdventOfCodeContext(2021);
            Assert.Equal(2021, context.Year);
        }

        [Fact]
        public async Task Initialize_EmptyDelegate_SetsCalendarUsingTheProvidedDataSource()
        {
            var context = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            await context.Initialize(context => { });
            Assert.Equal(Fixtures.Calendar, context.Calendar);
        }

        [Fact]
        public async Task Initialize_Delegate_CallsDelegateWithCurrentContext()
        {
            var currentContext = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            await currentContext.Initialize(context => {
                Assert.Equal(context, currentContext);
            });
        }

        [Fact]
        public async Task Initialize_AlreadyInitialized_ThrowsInvalidOperationException()
        {
            var context = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            await context.Initialize(c => { });
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(async () => await context.Initialize(c => { }));
            Assert.Equal(AdventOfCodeContext.AlreadyInitializedErrorMessage, thrown.Message);
        }

        [Fact]
        public void Submit_NotInitialized_ThrowsInvalidOperationException()
        { 
            var context = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            Assert.Throws<InvalidOperationException>(() => context.Submit<Fixtures.FirstPuzzle>());
        }

        [Fact]
        public async Task Submit_ValidType_ReturnsPuzzleSubmission()
        {
            var context = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            await context.Initialize(c => { });
            Assert.NotNull(context.Submit<Fixtures.FirstPuzzle>());
        }

        [Fact]
        public async Task Submit_ValidType_CallingSubmissionSetsPuzzleWhenCalledForKnownDay()
        {
            var context = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            await context.Initialize(c => { });
            context.Submit<Fixtures.FirstPuzzle>().ForDay(1);
            if (context.Calendar != null)
            { 
                Assert.IsType<Fixtures.FirstPuzzle>(context.Calendar[1].Puzzle);
            }
        }

        [Fact]
        public async Task Submit_ValidType_CallingSubmissionThrowsWhenCalledForUnknownDay()
        {
            var context = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            await context.Initialize(c => { });
            Assert.Throws<ArgumentOutOfRangeException>(() => context.Submit<Fixtures.FirstPuzzle>().ForDay(26));
        }

        [Fact]
        public void RegisterPuzzleFor_NotInitialized_ThrowsInvalidOperationException()
        {
            var context = new AdventOfCodeContext(new FixtureDataSource(), null, 2021);
            Assert.Throws<InvalidOperationException>(() => context.RegisterPuzzleForDay(1, new Fixtures.FirstPuzzle()));
        }

    }
}
