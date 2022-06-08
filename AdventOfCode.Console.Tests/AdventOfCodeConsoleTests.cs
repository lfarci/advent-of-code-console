using System;
using Xunit;

namespace AdventOfCode.Console.Tests
{
    public class AdventOfCodeConsoleTests
    {

        [Fact]
        public void StartYear_NewYear_SavesNewContextForTheGivenYear()
        {
            var application = new AdventOfCodeConsole();
            application.StartYear(2021, context => { });
            Assert.Equal(2021, application.FindCalendar(2021).Year);
        }

        [Fact]
        public void StartYear_NewYear_CallsDelegateWithContextOfTheGivenYear()
        {
            var application = new AdventOfCodeConsole();
            application.StartYear(2021, context =>
            {
                Assert.Equal(2021, context.Year);
            });
        }

        [Fact]
        public void StartYear_AddSameYearTwice_ThrowsInvalidOperationException()
        {
            var application = new AdventOfCodeConsole();
            application.StartYear(2021, context => { });
            Assert.Throws<InvalidOperationException>(() => application.StartYear(2021, year => { }));
        }
    }
}
