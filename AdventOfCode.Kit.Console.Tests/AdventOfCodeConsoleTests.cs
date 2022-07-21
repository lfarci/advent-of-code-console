using System;
using Xunit;

namespace AdventOfCode.Kit.Console.Tests
{
    public class AdventOfCodeConsoleTests
    {

        [Fact]
        public void FindSubmitter_NoContexts_Throws()
        {
            var console = new AdventOfCodeConsole();
            Assert.Throws<InvalidOperationException>(() => console.FindSubmitter(2021));
        }

        [Fact]
        public void FindSubmitter_UniqueYear_ReturnsContextForUniqueYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => { });
            Assert.Equal(2021, console.FindSubmitter(2021).Year);
        }

        [Fact]
        public void FindSubmitter_MultipleYears_SavesNewSubmitterForTheGivenYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2019, context => { });
            console.StartYear(2020, context => { });
            console.StartYear(2021, context => { });
            Assert.Equal(2019, console.FindSubmitter(2019).Year);
            Assert.Equal(2020, console.FindSubmitter(2020).Year);
            Assert.Equal(2021, console.FindSubmitter(2021).Year);
        }

        [Fact]
        public void StartYear_NoSubmitters_CallsDelegateWithContextOfTheGivenYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => Assert.Equal(2021, context.Year));
        }

        [Fact]
        public void StartYear_NoContexts_SavesNewSubmitterForTheSpecifiedYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => { });
            Assert.True(console.Submitters.ContainsKey(2021));
        }

        [Fact]
        public void StartYear_NoSubmitter_SavesOnlyOneSubmitter()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => { });
            Assert.Equal(1, console.Submitters.Count);
        }

        [Fact]
        public void StartYear_MultipleSubmitters_SavesMultipleSubmittersForTheSpecifiedYears()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2019, context => { });
            console.StartYear(2020, context => { });
            console.StartYear(2021, context => { });
            Assert.True(console.Submitters.ContainsKey(2019));
            Assert.True(console.Submitters.ContainsKey(2020));
            Assert.True(console.Submitters.ContainsKey(2021));
        }

        [Fact]
        public void StartYear_MultipleSubmitters_SavesOnlySubmittersForStartedYears()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2019, context => { });
            console.StartYear(2020, context => { });
            console.StartYear(2021, context => { });
            Assert.Equal(3, console.Submitters.Count);
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
