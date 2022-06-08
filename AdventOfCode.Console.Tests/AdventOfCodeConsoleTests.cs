using System;
using Xunit;

namespace AdventOfCode.Console.Tests
{
    public class AdventOfCodeConsoleTests
    {

        [Fact]
        public void FindContext_NoContexts_Throws()
        {
            var console = new AdventOfCodeConsole();
            Assert.Throws<InvalidOperationException>(() => console.FindContext(2021));
        }

        [Fact]
        public void FindContext_UniqueYear_ReturnsContextForUniqueYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => { });
            Assert.Equal(2021, console.FindContext(2021).Year);
        }

        [Fact]
        public void FindContext_MultipleYears_SavesNewContextForTheGivenYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2019, context => { });
            console.StartYear(2020, context => { });
            console.StartYear(2021, context => { });
            Assert.Equal(2019, console.FindContext(2019).Year);
            Assert.Equal(2020, console.FindContext(2020).Year);
            Assert.Equal(2021, console.FindContext(2021).Year);
        }

        [Fact]
        public void StartYear_NoContexts_CallsDelegateWithContextOfTheGivenYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => Assert.Equal(2021, context.Year));
        }

        [Fact]
        public void StartYear_NoContexts_SavesNewContextForTheSpecifiedYear()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => { });
            Assert.True(console.Contexts.ContainsKey(2021));
        }

        [Fact]
        public void StartYear_NoContexts_SavesOnlyOneContext()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2021, context => { });
            Assert.Equal(1, console.Contexts.Count);
        }

        [Fact]
        public void StartYear_MultipleContexts_SavesMultipleContextsForTheSpecifiedYears()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2019, context => { });
            console.StartYear(2020, context => { });
            console.StartYear(2021, context => { });
            Assert.True(console.Contexts.ContainsKey(2019));
            Assert.True(console.Contexts.ContainsKey(2020));
            Assert.True(console.Contexts.ContainsKey(2021));
        }

        [Fact]
        public void StartYear_MultipleContexts_SavesOnlyContextsForStartedYears()
        {
            var console = new AdventOfCodeConsole();
            console.StartYear(2019, context => { });
            console.StartYear(2020, context => { });
            console.StartYear(2021, context => { });
            Assert.Equal(3, console.Contexts.Count);
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
