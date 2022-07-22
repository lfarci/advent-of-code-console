using AdventOfCode.Kit.Client.Core;
using AdventOfCode.Kit.Client.Web.Resources;
using AdventOfCodeConsole.Tests.Helpers;
using System;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Web.Resources
{
    public class DayPageTests
    {
        private static readonly string notStartedDayPage = "AdventOfCode.Kit.Client.Tests.Fixtures.NotStartedDayPage.html";
        private static readonly string completeDayPage = "AdventOfCode.Kit.Client.Tests.Fixtures.CompleteDayPage.html";
        private static readonly string veryCompleteDayPage = "AdventOfCode.Kit.Client.Tests.Fixtures.VeryCompleteDayPage.html";

        [Fact]
        public void ParseTitle_EmptyString_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => DayPage.ParseTitle(""));
        }

        [Fact]
        public void ParseTitle_StringWithUnexpectedSeparator_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => DayPage.ParseTitle("Day 1 ; My Title"));
        }

        [Fact]
        public void ParseTitle_StringWithoutSeparator_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => DayPage.ParseTitle("Day 1 My Title"));
        }

        [Fact]
        public void ParseTitle_StringWithMoreThanOneSeparator_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => DayPage.ParseTitle("Day 1 : My Broken:Title"));
        }

        [Fact]
        public void ParseTitle_StringWithDecorator_ReturnsTitleWithoutDecorators()
        {
            Assert.Equal("My Title", DayPage.ParseTitle("--- Day 1 : My Title ---"));
        }

        [Fact]
        public void ParseTitle_StringWithDecoratorAndWhiteSpaces_ReturnsTrimmedTitle()
        {
            Assert.Equal("My Title", DayPage.ParseTitle("--- Day 1 :            My Title ---     "));
        }

        [Theory]
        [InlineData("")]
        [InlineData("<body></body>")]
        [InlineData("<main><article></article></main>")]
        [InlineData("<body><main><article></article></main></body>")]
        [InlineData("<body><main><article class='invalid'></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'></article></main></body>")]
        public void Parse_PageWithoutATitle_ThrowsTitleNotFoundError(string page)
        {
            var exception = Assert.Throws<FormatException>(() => DayPage.Parse(page));
            Assert.Equal(DayPage.TitleNotFoundError, exception.Message);
        }

        [Theory]
        [InlineData("<body><main><article class='day-desc'><h2></h2></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'><h2>Day 1 ; My Title</h2></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'><h2>Day 1 My Title</h2></article></main></body>")]
        [InlineData("<body><main><article class='day-desc'><h2>Day 1 : My Broken:Title</h2></article></main></body>")]
        public void Parse_PageWithInvalidTitle_ThrowsInvalidTitleError(string page)
        {
            var exception = Assert.Throws<FormatException>(() => DayPage.Parse(page));
            Assert.Contains(DayPage.InvalidTitleFormatError, exception.Message);
        }

        [Theory]
        [InlineData(@"<body><main>
                        <article class='day-desc'><h2>Day 1 : My Title</h2></article>
                        <p>Your puzzle answer was <code>invalid first answer</code></p>
                        <article class='day-desc'><h2>Part Two</h2></article>
                        <form method='post'></form>
                    </main></body>")]
        [InlineData(@"<body><main>
                        <article class='day-desc'><h2>Day 1 : My Title</h2></article>
                        <p>Your puzzle answer was <code>21234</code></p>
                        <article class='day-desc'><h2>Part Two</h2></article>
                        <p>Your puzzle answer was <code>invalid second answer</code></p>
                    </main></body>")]
        public void Parse_PageWithInvalidPuzzleAnswer_ThrowsFormatException(string page)
        {
            var exception = Assert.Throws<FormatException>(() => DayPage.Parse(page));
            Assert.Contains(DayPage.InvalidPuzzleAnswerFormatError, exception.Message);
        }

        [Theory]
        [InlineData(@"<body><main>
                        <article class='day-desc'><h2>Day 1 : My Title</h2></article>
                        <p>Your puzzle answer was</p>
                        <article class='day-desc'><h2>Part Two</h2></article>
                        <form method='post'></form>
                    </main></body>")]
        [InlineData(@"<body><main>
                        <article class='day-desc'><h2>Day 1 : My Title</h2></article>
                        <p>Your puzzle answer was <code>21234</code></p>
                        <article class='day-desc'><h2>Part Two</h2></article>
                        <p>Your puzzle answer was</p>
                    </main></body>")]
        [InlineData(@"<body><main>
                        <article class='day-desc'><h2>Day 1 : My Title</h2></article>
                        <p>Your puzzle answer was <code>21234</code></p>
                        <article class='day-desc'><h2>Part Two</h2></article>
                    </main></body>")]
        public void Parse_PageWithoutExpectedPuzzleAnswer_ThrowsFormatException(string page)
        {
            var exception = Assert.Throws<FormatException>(() => DayPage.Parse(page));
            Assert.Contains(DayPage.UnexpectedPuzzleAnswersCountError, exception.Message);
        }

        [Fact]
        public void Parse_CompleteDayPage_ReturnsDayWithExpectedTitle()
        {
            AssertParsedTitleEqual("Seven Segment Search", completeDayPage);
        }

        [Fact]
        public void Parse_CompleteDayPage_ReturnsDayWithExpectedCompletion()
        {
            WithHtmlPageResource(completeDayPage, day => Assert.Equal(Completion.Complete, day.Completion));
        }

        [Fact]
        public void Parse_CompleteDayPage_ReturnsDayWithExpectedFirstPuzzleAnswer()
        {
            WithHtmlPageResource(completeDayPage, day => Assert.Equal(412, day.FirstPuzzleAnswer));
        }

        [Fact]
        public void Parse_CompleteDayPageWithBigValue_ReturnsDayWithExpectedFirstPuzzleAnswer()
        {
            string page = @"<body><main>
                <article class='day-desc'><h2>Day 1 : My Title</h2></article>
                <p>Your puzzle answer was <code>1595330616005</code></p>
                <article class='day-desc'><h2>Part Two</h2></article>
                <form method='post'></form>
            </main></body>";
            WithHtmlPage(page, day => Assert.Equal(1595330616005, day.FirstPuzzleAnswer));
        }

        [Fact]
        public void Parse_CompleteDayPage_ReturnsDayWithoutSecondPuzzleAnswer()
        {
            WithHtmlPageResource(completeDayPage, day => Assert.Null(day.SecondPuzzleAnswer));
        }

        [Fact]
        public void Parse_VeryCompleteDayPage_ReturnsDayWithExpectedTitle()
        {
            AssertParsedTitleEqual("Sonar Sweep", veryCompleteDayPage);
        }

        [Fact]
        public void Parse_VeryCompleteDayPage_ReturnsDayWithExpectedCompletion()
        {
            WithHtmlPageResource(veryCompleteDayPage, day => Assert.Equal(Completion.VeryComplete, day.Completion));
        }

        [Fact]
        public void Parse_VeryCompleteDayPage_ReturnsDayWithExpectedFirstPuzzleAnswer()
        {
            WithHtmlPageResource(veryCompleteDayPage, day => Assert.Equal(1448, day.FirstPuzzleAnswer));
        }

        [Fact]
        public void Parse_VeryCompleteDayPage_ReturnsDayWithExpectedSecondPuzzleAnswer()
        {
            WithHtmlPageResource(veryCompleteDayPage, day => Assert.Equal(1471, day.SecondPuzzleAnswer));
        }

        [Fact]
        public void Parse_NotStartedDayPage_ReturnsDayWithExpectedTitle()
        {
            AssertParsedTitleEqual("Smoke Basin", notStartedDayPage);
        }

        [Fact]
        public void Parse_NotStartedDayPage_ReturnsDayWithExpectedCompletion()
        {
            WithHtmlPageResource(notStartedDayPage, day => Assert.Equal(Completion.NotStarted, day.Completion));
        }

        [Fact]
        public void Parse_NotStartedDayPage_ReturnsDayWithoutFirstPuzzleAnswer()
        {
            WithHtmlPageResource(notStartedDayPage, day => Assert.Null(day.FirstPuzzleAnswer));
        }

        [Fact]
        public void Parse_NotStartedDayPage_ReturnsDayWithoutSecondPuzzleAnswer()
        {
            WithHtmlPageResource(notStartedDayPage, day => Assert.Null(day.SecondPuzzleAnswer));
        }

        private static void AssertParsedTitleEqual(string expectedTitle, string resourceName)
        {
            WithHtmlPageResource(resourceName, day => Assert.Equal(expectedTitle, day.Title));
        }

        private static void WithHtmlPageResource(string resourceName, Action<DayPage> assertCallback)
        {
            var page = Helpers.ReadResourceContentAsString(resourceName);
            WithHtmlPage(page, assertCallback);
        }

        private static void WithHtmlPage(string page, Action<DayPage> assertCallback)
        {
            assertCallback(DayPage.Parse(page));
        }
    }
}
