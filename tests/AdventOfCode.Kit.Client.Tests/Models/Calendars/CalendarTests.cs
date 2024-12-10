using AdventOfCode.Kit.Client.Models;
using AdventOfCodeConsole.Tests.Helpers;
using System;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Models
{
    public class CalendarTests
    {
        private readonly static Calendar calendar = Fixtures.Calendar;

        [Fact]
        public void Indexer_FirstIndex_ReturnsFirstDay()
        { 
            Assert.Equal("Day 1", calendar[1].Title);
        }

        [Fact]
        public void Indexer_LastIndex_ReturnsLastDay()
        {
            Assert.Equal("Day 10", calendar[10].Title);
        }

        [Fact]
        public void Indexer_NegativeIndex_ThrowsOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => calendar[-1]);
        }

        [Fact]
        public void Indexer_IndexIsEqualToLength_ThrowsOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => calendar[26]);
        }
    }
}
