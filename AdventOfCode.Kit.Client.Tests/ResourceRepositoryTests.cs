using AdventOfCode.Kit.Client.Web.Resources;
using AdventOfCodeConsole.Tests.Helpers;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Web.Resources
{
    public class ResourceRepositoryTests
    {
        [Fact]
        public async Task FindCalendarPageByYearAsync_IOExceptionIsThrown_ThrowsAgain()
        {
            Expression<Action<ICalendarPageRepository>> call = c => c.FindByYearAsync(Fixtures.DefaultYear);
            var resources = Mocks.GetRepositoryThatThrows<IOException>(call);
            await Assert.ThrowsAsync<IOException>(async () => await resources.FindCalendarPageByYearAsync(Fixtures.DefaultYear));
        }

        [Fact]
        public async Task FindCalendarPageByYearAsync_InvalidOperationExceptionIsThrown_ThrowsAgain()
        {
            Expression<Action<ICalendarPageRepository>> call = c => c.FindByYearAsync(Fixtures.DefaultYear);
            var resources = Mocks.GetRepositoryThatThrows<InvalidOperationException>(call);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await resources.FindCalendarPageByYearAsync(Fixtures.DefaultYear));
        }

        [Fact]
        public async Task FindCalendarPageByYearAsync_ValidResponse_ReturnsCalendarPage()
        {
            var calendar = new CalendarPage(Fixtures.CalendarPageEntries) { Year = Fixtures.DefaultYear };
            var resources = Mocks.GetRepositoryThatReturns(calendar);
            Assert.True(calendar.Equals(await resources.FindCalendarPageByYearAsync(Fixtures.DefaultYear)));
        }


        [Fact]
        public async Task FindDayPageByYearAndDayAsync_IOExceptionIsThrown_ThrowsAgain()
        {
            Expression<Action<IDayPageRepository>> call = c => c.FindByYearAndDayAsync(Fixtures.DefaultYear, Fixtures.DefaultDay);
            var resources = Mocks.GetRepositoryThatThrows<IOException>(call);
            await Assert.ThrowsAsync<IOException>(async () => await resources.FindDayPageByYearAndDayAsync(Fixtures.DefaultYear, Fixtures.DefaultDay));
        }

        [Fact]
        public async Task FindDayPageByYearAndDayAsync_InvalidOperationExceptionIsThrown_ThrowsAgain()
        {
            Expression<Action<IDayPageRepository>> call = c => c.FindByYearAndDayAsync(Fixtures.DefaultYear, Fixtures.DefaultDay);
            var resources = Mocks.GetRepositoryThatThrows<InvalidOperationException>(call);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await resources.FindDayPageByYearAndDayAsync(Fixtures.DefaultYear, Fixtures.DefaultDay));
        }

        [Fact]
        public async Task FindDayPageByYearAndDayAsync_ValidResponse_ReturnsDayPagePage()
        {
            var dayPage = new DayPage { Title = "My title" };
            var resources = Mocks.GetRepositoryThatReturns(dayPage);
            Assert.True(dayPage.Equals(await resources.FindDayPageByYearAndDayAsync(2021, 1)));
        }

        [Fact]
        public async Task FindPuzzleInputByYearAndDayAsync_IOExceptionIsThrown_ThrowsAgain()
        {
            Expression<Action<IPuzzleInputRepository>> call = c => c.FindByYearAndDayAsync(Fixtures.DefaultYear, Fixtures.DefaultDay);
            var resources = Mocks.GetRepositoryThatThrows<IOException>(call);
            await Assert.ThrowsAsync<IOException>(async () => await resources.FindPuzzleInputByYearAndDayAsync(Fixtures.DefaultYear, Fixtures.DefaultDay));
        }

        [Fact]
        public async Task FindPuzzleInputByYearAndDayAsync_ValidResponse_ReturnsPuzzleInput()
        {
            var lines = new string[] { "line0", "line1", "line2" };
            var resources = Mocks.GetRepositoryThatReturns(lines);
            var returnedLines = await resources.FindPuzzleInputByYearAndDayAsync(2021, 1);
            Assert.Contains("line0", returnedLines);
            Assert.Contains("line1", returnedLines);
            Assert.Contains("line2", returnedLines);
        }

    }
}
