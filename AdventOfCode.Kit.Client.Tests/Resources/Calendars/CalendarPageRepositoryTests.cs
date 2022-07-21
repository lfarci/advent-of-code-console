using AdventOfCode.Kit.Client.Resources;
using AdventOfCodeConsole.Tests.Helpers;
using System;
using System.IO;
using Xunit;

namespace AdventOfCode.Kit.Console.Tests.Web
{
    public class CalendarPageRepositoryTests
    {
        private static readonly int defaultYear = Fixtures.DefaultYear;
        private static readonly string resourceName = "AdventOfCode.Kit.Client.Tests.Fixtures.CalendarPage.html";
        private static readonly string validPage = Helpers.ReadResourceContentAsString(resourceName);

        [Fact]
        public void Instance_ReturnsSingleton()
        {
            Assert.Equal(CalendarPageRepository.Instance, CalendarPageRepository.Instance);
        }

        [Fact]
        public async void FindByYearAsync_ClientThrowsIOException_ThrowsIOException()
        {
            var repository = Helpers.GetCalendarPageRepositoryThatThrows<IOException>();
            var thrown = await Assert.ThrowsAsync<IOException>(() => repository.FindByYearAsync(defaultYear));
            Assert.Equal(CalendarPageRepository.GetNotFoundErrorMessage(defaultYear), thrown.Message);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsInvalidPage_ThrowsInvalidOperationException()
        {
            var repository = Helpers.GetCalendarPageRepositoryThatReturns("Invalid page");
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() => repository.FindByYearAsync(defaultYear));
            Assert.Equal(CalendarPageRepository.GetParseErrorMessage(defaultYear), thrown.Message);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsValidPage_ReturnsCalendar()
        {
            var repository = Helpers.GetCalendarPageRepositoryThatReturns(validPage);
            var calendar = await repository.FindByYearAsync(defaultYear);
            Assert.NotNull(calendar);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsValidPage_ReturnsCalendarWithDefaultYear()
        {
            var repository = Helpers.GetCalendarPageRepositoryThatReturns(validPage);
            var calendar = await repository.FindByYearAsync(defaultYear);
            Assert.Equal(defaultYear, calendar.Year);
        }
    }
}
