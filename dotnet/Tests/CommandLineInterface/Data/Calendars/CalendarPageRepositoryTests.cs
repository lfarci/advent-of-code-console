using CommandLineInterface.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.CommandLineInterface.Data
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
            var repository = GetRepositoryThatThrows(new IOException());
            var thrown = await Assert.ThrowsAsync<IOException>(() => repository.FindByYearAsync(defaultYear));
            Assert.Equal($"Could not find cannot calendar for year {defaultYear}.", thrown.Message);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsInvalidPage_ThrowsInvalidOperationException()
        {
            var repository = GetRepositoryThatReturns("Invalid page");
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(() => repository.FindByYearAsync(defaultYear));
            Assert.Equal($"Could not parse calendar for year {defaultYear}.", thrown.Message);
        }

        [Fact]
        public async void FindByYearAsync_ClientReturnsValidPage_ReturnsCalendar()
        {
            var repository = GetRepositoryThatReturns(validPage);
            var calendar = await repository.FindByYearAsync(defaultYear);
            Assert.NotNull(calendar);
        }

        private static CalendarPageRepository GetRepositoryThatReturns(string response)
        {
            var repositoryMock = new Mock<CalendarPageRepository>();
            repositoryMock.CallBase = true;

            repositoryMock
                .Setup(r => r.FindCalendarPageAsStreamAsync(defaultYear).Result)
                .Returns(Helpers.GenerateStreamFromString(response));

            return repositoryMock.Object;
        }

        private static CalendarPageRepository GetRepositoryThatThrows(Exception exception)
        {
            var repositoryMock = new Mock<CalendarPageRepository>();
            repositoryMock.CallBase = true;

            repositoryMock
                .Setup(r => r.FindCalendarPageAsStreamAsync(defaultYear).Result)
                .Throws(exception);

            return repositoryMock.Object;
        }
    }
}
