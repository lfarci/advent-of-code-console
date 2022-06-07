using AdventOfCode.Challenges;
using System;
using Xunit;

namespace Tests.Challenges.Day02
{
    public class DiveChallengeTests
    {
        private static string[] instructions = {
            "forward 5",
            "down 5",
            "forward 8",
            "up 3",
            "down 8",
            "forward 2"
        };

        [Fact]
        public void FollowInstructionsFrom_CoordinateAndValidInstructions_RetunsCoordinateWithExpectedX()
        {
            var departure = new Coordinate(0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(15, arrival.X);
        }

        [Fact]
        public void FollowInstructionsFrom_CoordinateAndValidInstructions_RetunsCoordinateWithExpectedY()
        {
            var departure = new Coordinate(0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(10, arrival.Y);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(34, 2)]
        [InlineData(-1, 34)]
        [InlineData(34, -1)]
        public void FollowInstructionsFrom_CoordinateAndNoInstructions_RetunsInputCoordinate(int x, int y)
        {
            var departure = new Coordinate(x, y);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, Array.Empty<string>());
            Assert.True(departure.Equals(arrival));
        }

        [Fact]
        public void FollowInstructionsFrom_ValidInstructions_RetunsCoordinateWithExpectedX()
        {
            var departure = new SubmarineCoordinate(0, 0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(15, arrival.X);
        }

        [Fact]
        public void FollowInstructionsFrom_ValidInstructions_RetunsCoordinateWithExpectedY()
        {
            var departure = new SubmarineCoordinate(0, 0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(60, arrival.Y);
        }
    }
}
