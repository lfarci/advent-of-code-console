using AdventOfCode.Console;
using AdventOfCode.Console.Core;
using AdventOfCodeConsole.Tests.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCodeConsole.Tests
{
    public class AdventOfCodeApplicationTests
    {

        [Fact]
        public void StartYear_NewYear_SavesNewContextForTheGivenYear()
        {
            var application = new AdventOfCodeApplication();
            application.StartYear(2021, context => {});
            Assert.Equal(2021, application.FindCalendar(2021).Year);
        }

        [Fact]
        public void StartYear_NewYear_CallsDelegateWithContextOfTheGivenYear()
        {
            var application = new AdventOfCodeApplication();
            application.StartYear(2021, context => {
                Assert.Equal(2021, context.Year);
            });
        }

        [Fact]
        public void StartYear_AddSameYearTwice_ThrowsInvalidOperationException()
        {
            var application = new AdventOfCodeApplication();
            application.StartYear(2021, context => {});
            Assert.Throws<InvalidOperationException>(() => application.StartYear(2021, year => {}));
        }
    }
}
