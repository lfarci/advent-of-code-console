using CommandLineInterface.Data;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

using static CommandLineInterface.Data.AdventOfCodeCalendar;

namespace Tests.CommandLineInterface.Data
{
    public class AdventOfCodeCalendarTests
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
            var calendar = new AdventOfCodeCalendar(expectedDays);
            Assert.True(calendar.Equals(Parse(calendarPage)));
        }

        [Fact]
        public void Parse_CalendarPage_ReturnsCalendarWithExpectedCount()
        {
            Assert.Equal(expectedDays.Count, Parse(calendarPage).Days.Count);
        }

        [Fact]
        public void ParseDayIndex_AnchorWithValidDayIndex_ReturnsParsedCalendar()
        {
            Assert.Equal(12, ParseDayIndex(BuildDayAnchorNode("12", Completion.VeryComplete)));
        }

        [Fact]
        public void ParseDayIndex_AnchorWithoutCalendarDay_ReturnsMinusOne()
        {
            var htmlNode = HtmlNode.CreateNode(@"<a href='/2020/day/1' class='calendar-day'>
                <span class='calendar-day-with-typo'>1</span>
            </a>");
            Assert.Equal(-1, ParseDayIndex(htmlNode));
        }

        [Fact]
        public void ParseDayIndex_AnchorWithInvalidStringInDay_ReturnsMinusOne()
        {
            Assert.Equal(-1, ParseDayIndex(BuildDayAnchorNode("invalid", Completion.VeryComplete)));
        }

        [Fact]
        public void ParseDayIndex_AnchorWithEmptyStringInDay_ReturnsMinusOne()
        {
            Assert.Equal(-1, ParseDayIndex(BuildDayAnchorNode("", Completion.VeryComplete)));
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
            var emptyCalendar = () => new AdventOfCodeCalendar(new List<Day>());
            Assert.True(emptyCalendar().Equals(emptyCalendar()));
        }

        [Fact]
        public void Equals_CalendarWithExtraDay_ReturnsIsNotEqual()
        {
            var first = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 1, Completion = Completion.VeryComplete },
                new Day { Index = 2, Completion = Completion.VeryComplete },
            });
            var second = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 1, Completion = Completion.VeryComplete },
                new Day { Index = 2, Completion = Completion.VeryComplete },
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            Assert.False(first().Equals(second()));
        }

        [Fact]
        public void Equals_CalendarWithSameCountButDifferentElements_ReturnsIsNotEqual()
        {
            var first = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete},
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            var second = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 1, Completion = Completion.VeryComplete },
                new Day { Index = 2, Completion = Completion.NotStarted },
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            Assert.False(first().Equals(second()));
        }

        [Fact]
        public void Equals_CalendarWithSameElements_ReturnsIsEqual()
        {
            var first = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete},
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            var second = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete },
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            Assert.True(first().Equals(second()));
        }

        [Fact]
        public void Equals_CalendarWithUnorderedSameElements_ReturnsIsEqual()
        {
            var first = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 1, Completion = Completion.NotStarted },
                new Day { Index = 2, Completion = Completion.Complete},
                new Day { Index = 3, Completion = Completion.VeryComplete },
            });
            var second = () => new AdventOfCodeCalendar(new List<Day>() {
                new Day { Index = 2, Completion = Completion.Complete },
                new Day { Index = 3, Completion = Completion.VeryComplete },
                new Day { Index = 1, Completion = Completion.NotStarted },
            });
            Assert.True(first().Equals(second()));
        }

        private static string ReadCalendarPage()
        {
            string calendarPage = "";
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                calendarPage = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            }
            return calendarPage;
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
