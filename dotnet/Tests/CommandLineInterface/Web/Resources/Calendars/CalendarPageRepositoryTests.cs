using AdventOfCode.Console.Web;
using System;
using System.IO;
using Xunit;

namespace Tests.CommandLineInterface.Web.Resources
{
    public class CalendarPageRepositoryTests
    {
        private static readonly int defaultYear = 2021;
        private static readonly string resourceName = "Tests.Resources.CalendarPage.html";
        private static readonly string validPage = Helpers.ReadResourceContentAsString(resourceName);

        [Fact]
        public void Instance_ReturnsSingleton()
        {
            Assert.Equal(CalendarPageRepository.Instance, CalendarPageRepository.Instance);
        }

        [Fact]
        public async void FindByYearAsync_ClientThrowsIOException_ThrowsIOException()
        {
            var repository = GetRepositoryThatThrows<IOException>();
            var thrown = await Assert.ThrowsAsync<IOException>(() => repository.FindByYearAsync(defaultYear));
            Assert.Equal(CalendarPageRepository.GetNotFoundErrorMessage(defaultYear), thrown.Message);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsInvalidPage_ThrowsInvalidOperationException()
        {
            var repository = GetRepositoryThatReturns("Invalid page");
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() => repository.FindByYearAsync(defaultYear));
            Assert.Equal(CalendarPageRepository.GetParseErrorMessage(defaultYear), thrown.Message);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsValidPage_ReturnsCalendar()
        {
            var repository = GetRepositoryThatReturns(validPage);
            var calendar = await repository.FindByYearAsync(defaultYear);
            Assert.NotNull(calendar);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsValidPage_ReturnsCalendarWithDefaultYear()
        {
            var repository = GetRepositoryThatReturns(validPage);
            var calendar = await repository.FindByYearAsync(defaultYear);
            Assert.Equal(defaultYear, calendar.Year);
        }

        private static ICalendarPageRepository GetRepositoryThatThrows<TException>() where TException : Exception, new()
        {
            var client = Helpers.GetClientThatThrows<TException>(c => c.GetCalendarPageAsStreamAsync(defaultYear));
            return new CalendarPageRepository(client);
        }

        private static ICalendarPageRepository GetRepositoryThatReturns(string result)
        {
            var client = Helpers.GetClientThatReturns(result, c => c.GetCalendarPageAsStreamAsync(defaultYear).Result);
            return new CalendarPageRepository(client);
        }
    }
}
