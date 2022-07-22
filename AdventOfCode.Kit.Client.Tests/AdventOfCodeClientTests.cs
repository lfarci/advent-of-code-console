using AdventOfCode.Kit.Client.Models;
using AdventOfCode.Kit.Client.Web;
using AdventOfCode.Kit.Client.Web.Resources;
using AdventOfCodeConsole.Tests.Helpers;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Models
{
    public class AdventOfCodeClientTests
    {

        private IConfiguration _configuration = new AdventOfCodeWebsiteConfiguration { 
            Host = "adventodcode.com",
            SessionId = "A session id"
        };

        [Fact]
        public async Task FindCalendarByYearAsync_ValidYear_ReturnsCalendarWithExpectedNumberOfDays()
        {
            var website = new AdventOfCodeClient(GetRepository());
            var calendar = await website.FindCalendarByYearAsync(2021);
            Assert.Equal(Fixtures.CalendarDays.Count(), calendar.Days.Count());
        }

        [Fact]
        public async Task FindCalendarByYearAsync_ValidYear_ReturnsCalendarWithExpectedLength()
        {
            var website = new AdventOfCodeClient(GetRepository());
            var calendar = await website.FindCalendarByYearAsync(2021);
            Assert.Equal(Fixtures.CalendarDays.Count(), calendar.Length);
        }

        [Fact]
        public async Task FindCalendarByYearAsync_ValidYear_ReturnsCalendarWithExpectedYear()
        {
            var website = new AdventOfCodeClient(GetRepository());
            var calendar = await website.FindCalendarByYearAsync(2021);
            Assert.Equal(Fixtures.CalendarPage.Year, calendar.Year);
        }

        private static IResourceRepository GetRepository()
        {
            var repository = new Mock<IResourceRepository>();

            repository
                .Setup(r => r.FindCalendarPageByYearAsync(It.IsAny<int>()).Result)
                .Returns(Fixtures.CalendarPage);

            foreach (var day in Fixtures.CalendarDays)
            {
                repository
                    .Setup(r => r.FindDayPageByYearAndDayAsync(It.IsAny<int>(), day.Index).Result)
                    .Returns(new DayPage
                    {
                        Title = day.Title,
                        Index = day.Index,
                        Completion = Completion.Complete,
                        FirstPuzzleAnswer = day.FirstPuzzleAnswer,
                        SecondPuzzleAnswer = day.SecondPuzzleAnswer,
                    });
            }

            return repository.Object;
        }
    }
}
