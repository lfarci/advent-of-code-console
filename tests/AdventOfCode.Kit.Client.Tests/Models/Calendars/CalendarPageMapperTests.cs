using AdventOfCode.Kit.Client.Models;
using AdventOfCode.Kit.Client.Web.Resources;
using System.Collections.Generic;
using Xunit;
using static AdventOfCode.Kit.Client.Web.Resources.CalendarPage;

namespace AdventOfCode.Kit.Client.Tests.Models
{
    public class CalendarPageMapperTests
    {
        private static readonly IList<DayEntry> days = new List<DayEntry>() {
            new DayEntry { Index = 1, Completion = Completion.VeryComplete },
            new DayEntry { Index = 2, Completion = Completion.VeryComplete },
            new DayEntry { Index = 3, Completion = Completion.VeryComplete },
        };

        [Theory]
        [InlineData(2016)]
        [InlineData(2018)]
        [InlineData(2022)]
        public void ToCalendar_CalendarPageForAYear_ReturnsCalendarForSameYear(int year)
        {
            Assert.Equal(year, CalendarPageMapper.ToCalendar(new CalendarPage { Year = year }).Year);
        }

        [Fact]
        public void ToCalendar_CalendarPageWithEmptyDaysList_ReturnsCalendarWithZeroLength()
        {
            Assert.Equal(0, CalendarPageMapper.ToCalendar(new CalendarPage()).Length);
        }

        [Fact]
        public void ToCalendar_CalendarPageWithDaysList_ReturnsCalendarWithExpectedCount()
        {
            Assert.Equal(days.Count, CalendarPageMapper.ToCalendar(new CalendarPage(days)).Length);
        }

        [Fact]
        public void ToCalendar_CalendarPageWithDaysList_ReturnsCalendarWithEmptyDays()
        {
            Assert.Equal(days.Count, CalendarPageMapper.ToCalendar(new CalendarPage(days)).Length);
        }

    }
}
