using CommandLineInterface.Data;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using Xunit;

using static CommandLineInterface.Data.CalendarPage;

namespace Tests.CommandLineInterface.Data
{
    public class CalendarPageTests
    {
        private static readonly string resourceName = "Tests.Resources.CalendarPage.html";
        private static readonly string calendarPage = ReadCalendarPage();
        private static readonly IList<Day> expectedDays = new List<Day>() {
            new Day { Index = 1, Completion = Completion.VeryComplete },
            new Day { Index = 2, Completion = Completion.VeryComplete },
            new Day { Index = 3, Completion = Completion.VeryComplete },
            new Day { Index = 4, Completion = Completion.VeryComplete },
            new Day { Index = 5, Completion = Completion.VeryComplete },
            new Day { Index = 6, Completion = Completion.VeryComplete },
            new Day { Index = 7, Completion = Completion.VeryComplete },
            new Day { Index = 8, Completion = Completion.VeryComplete },
            new Day { Index = 9, Completion = Completion.VeryComplete },
            new Day { Index = 10, Completion = Completion.Complete },
            new Day { Index = 11, Completion = Completion.NotStarted },
            new Day { Index = 12, Completion = Completion.NotStarted },
            new Day { Index = 13, Completion = Completion.NotStarted },
            new Day { Index = 14, Completion = Completion.NotStarted },
            new Day { Index = 15, Completion = Completion.NotStarted },
            new Day { Index = 16, Completion = Completion.NotStarted },
            new Day { Index = 17, Completion = Completion.NotStarted },
            new Day { Index = 18, Completion = Completion.NotStarted },
            new Day { Index = 19, Completion = Completion.NotStarted },
            new Day { Index = 20, Completion = Completion.NotStarted },
            new Day { Index = 21, Completion = Completion.NotStarted },
            new Day { Index = 22, Completion = Completion.NotStarted },
            new Day { Index = 23, Completion = Completion.NotStarted },
            new Day { Index = 24, Completion = Completion.NotStarted },
            new Day { Index = 25, Completion = Completion.NotStarted }
        };

        [Fact]
        public void Parse_CalendarPage_ReturnsParsedCalendar()
        {
            var calendar = new CalendarPage(expectedDays);
            Assert.True(calendar.Equals(Parse(calendarPage)));
        }

        [Theory]
        [InlineData("")]
        [InlineData("<main><pre></pre></main>")]
        [InlineData("<body><main><pre></pre></main></body>")]
        [InlineData("<body><main><pre class='unknown'></pre></main></body>")]
        [InlineData("<body><main><div class='calendar'></div></main></body>")]
        [InlineData("<body><main><pre class='calendar'></pre></main></body>")]
        public void Parse_InvalidHtmlPage_ThrowsFormatException(string text)
        {
            Assert.Throws<FormatException>(() => Parse(text));
        }

        [Fact]
        public void ParseDayIndex_AnchorWithValidDayIndex_ReturnsParsedCalendar()
        {
            Assert.Equal(12, ParseDayIndex(BuildDayAnchorNode("12", Completion.VeryComplete)));
        }

        [Fact]
        public void ParseDayIndex_AnchorWithoutCalendarDay_ThrowsFormatException()
        {
            var htmlNode = HtmlNode.CreateNode(@"<a href='/2020/day/1' class='calendar-day'>
                <span class='calendar-day-with-typo'>1</span>
            </a>");
            Assert.Throws<FormatException>(() => ParseDayIndex(htmlNode));
        }

        [Fact]
        public void ParseDayIndex_AnchorWithInvalidStringInDay_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => ParseDayIndex(BuildDayAnchorNode("invalid", Completion.VeryComplete)));

        }

        [Fact]
        public void ParseDayIndex_AnchorWithEmptyStringInDay_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => ParseDayIndex(BuildDayAnchorNode("", Completion.VeryComplete)));
        }

        [Fact]
        public void ParseCompletion_AnchorHasCompleteClassName_ReturnsComplete()
        {
            var completion = Completion.Complete;
            Assert.Equal(completion, ParseCompletion(BuildDayAnchorNode("12", completion)));
        }

        [Fact]
        public void ParseCompletion_AnchorHasVeryCompleteClassName_ReturnsVeryComplete()
        {
            var completion = Completion.VeryComplete;
            Assert.Equal(completion, ParseCompletion(BuildDayAnchorNode("12", completion)));
        }

        [Fact]
        public void ParseCompletion_AnchorHasNoCompletionClassName_ReturnsNotStarted()
        {
            var completion = Completion.NotStarted;
            Assert.Equal(completion, ParseCompletion(BuildDayAnchorNode("12", completion)));
        }

        [Fact]
        public void ParseCompletion_AnchorHasNoClassAttributes_ReturnsNotStarted()
        {
            var htmlNode = HtmlNode.CreateNode(@"<a href='/2020/day/1'></a>");
            Assert.Equal(Completion.NotStarted, ParseCompletion(htmlNode));
        }

        [Fact]
        public void Equals_EmptyCalendar_ReturnsIsEquals()
        {
            var emptyCalendar = () => new CalendarPage(new List<Day>());
            Assert.True(emptyCalendar().Equals(emptyCalendar()));
        }

        [Fact]
        public void Equals_CalendarWithExtraDay_ReturnsIsNotEqual()
        {
            var first = () => new CalendarPage(new List<Day>() {
                new Day { Index = 1, Completion = Completion.VeryComplete },
                new Day { Index = 2, Completion = Completion.VeryComplete },
            });
            var second = () => new CalendarPage(new List<Day>() {
                new Day { Index = 1, Completion = Completion.VeryComplete },
                new Day { Index = 2, Completion = Completion.VeryComplete },
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            Assert.False(first().Equals(second()));
        }

        [Fact]
        public void Equals_CalendarWithSameCountButDifferentElements_ReturnsIsNotEqual()
        {
            var first = () => new CalendarPage(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete},
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            var second = () => new CalendarPage(new List<Day>() {
                new Day { Index = 1, Completion = Completion.VeryComplete },
                new Day { Index = 2, Completion = Completion.NotStarted },
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            Assert.False(first().Equals(second()));
        }

        [Fact]
        public void Equals_CalendarWithSameElements_ReturnsIsEqual()
        {
            var first = () => new CalendarPage(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete},
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            var second = () => new CalendarPage(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete },
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            Assert.True(first().Equals(second()));
        }

        [Fact]
        public void Equals_CalendarWithUnorderedSameElements_ReturnsIsEqual()
        {
            var first = () => new CalendarPage(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete},
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            var second = () => new CalendarPage(new List<Day>() {
                new Day { Index = 2, Completion = Completion.Complete },
                new Day { Index = 3, Completion = Completion.VeryComplete },
                new Day { Index = 1, Completion = Completion.NotStarted },
            });
            Assert.True(first().Equals(second()));
        }

        private static string ReadCalendarPage()
        {
            return Helpers.ReadResourceContentAsString(resourceName);
        }

        private static string ToClassName(Completion completion)
        {
            var className = "";
            if (completion == Completion.Complete)
            {
                className = "calendar-complete";
            }
            if (completion == Completion.VeryComplete)
            {
                className = "calendar-verycomplete";
            }
            return className;
        }

        private static HtmlNode BuildDayAnchorNode(string index, Completion completion)
        {
            var html = @"
            <a aria-label='Day 1, two stars' href='/2020/day/1' class='calendar-day1 " + ToClassName(completion) + @"'>
                <span class='calendar-color-l'>..........</span>
                <span class='calendar-color-r'>|</span>
                <span class='calendar-color-l'>..........</span>
                <span class='calendar-day'>" + index + @"</span>
                <span class='calendar-mark-complete'>*</span>
                <span class='calendar-mark-verycomplete'>*</span>
            </a>";
            return HtmlNode.CreateNode(html);
        }
    }
}
