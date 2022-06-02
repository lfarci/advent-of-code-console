using CommandLineInterface.Data.Days;
using System;
using Tests.Helpers;
using Xunit;

namespace Tests.CommandLineInterface.Data
{
    public class DayTests
    {
        private static readonly string notStartedDayPage = "Tests.Resources.NotStartedDayPage.html";
        private static readonly string completeDayPage = "Tests.Resources.CompleteDayPage.html";
        private static readonly string veryCompleteDayPage = "Tests.Resources.VeryCompleteDayPage.html";

        [Fact]
        public void ParseTitle_EmptyString_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => Day.ParseTitle(""));
        }

        [Fact]
        public void ParseTitle_StringWithUnexpectedSeparator_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => Day.ParseTitle("Day 1 ; My Title"));
        }

        [Fact]
        public void ParseTitle_StringWithoutSeparator_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => Day.ParseTitle("Day 1 My Title"));
        }

        [Fact]
        public void ParseTitle_StringWithMoreThanOneSeparator_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => Day.ParseTitle("Day 1 : My Broken:Title"));
        }

        [Fact]
        public void ParseTitle_StringWithDecorator_ReturnsTitleWithoutDecorators()
        {
            Assert.Equal("My Title", Day.ParseTitle("--- Day 1 : My Title ---"));
        }

        [Fact]
        public void ParseTitle_StringWithDecoratorAndWhiteSpaces_ReturnsTrimmedTitle()
        {
            Assert.Equal("My Title", Day.ParseTitle("--- Day 1 :            My Title ---     "));
        }

        [Theory]
        [InlineData("")]
        [InlineData("<body></body>")]
        [InlineData("<main><article></article></main>")]
        [InlineData("<body><main><article></article></main></body>")]
        [InlineData("<body><main><article class='invalid'></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'><h2></h2></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'><h2>Day 1 ; My Title</h2></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'><h2>Day 1 My Title</h2></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'><h2>Day 1 : My Broken:Title</h2></article></main></body>")]
        public void Parse_InvalidPage_ThrowsFormatException(string page)
        { 
            Assert.Throws<FormatException>(() => Day.Parse(page));
        }

        [Fact]
        public void Parse_CompleteDayPage_ReturnsDayWithExpectedTitle()
        {
            AssertParsedTitleEqual("Seven Segment Search", completeDayPage);
        }

        [Fact]
        public void Parse_VeryCompleteDayPage_ReturnsDayWithExpectedTitle()
        {
            AssertParsedTitleEqual("Sonar Sweep", veryCompleteDayPage);
        }


        [Fact]
        public void Parse_NotStartedDayPage_ReturnsDayWithExpectedTitle()
        {
            AssertParsedTitleEqual("Smoke Basin", notStartedDayPage);
        }

        private static void AssertParsedTitleEqual(string expectedTitle, string resourceName)
        {
            string page = TestHelpers.ReadResourceContentAsString(resourceName);
            Assert.Equal(expectedTitle, Day.Parse(page).Title);
        }
    }
}
