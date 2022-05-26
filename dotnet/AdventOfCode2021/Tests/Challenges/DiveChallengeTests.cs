using Xunit;
using AdventOfCode2021.Challenges;
using System;

namespace Tests.Challenges
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
        public void ParseInstructionFrom_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.ParseInstructionFrom(""));
        }

        [Fact]
        public void ParseInstructionFrom_InvalidInstruction_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.ParseInstructionFrom("3 back"));
        }


        [Fact]
        public void ParseInstructionFrom_UnknownInstruction_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.ParseInstructionFrom("unknown 3"));
        }

        [Fact]
        public void ParseInstructionFrom_SingleToken_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.ParseInstructionFrom("forward"));
        }

        [Fact]
        public void ParseInstructionFrom_ThreeTokens_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.ParseInstructionFrom("forward 2 fast"));
        }

        [Fact]
        public void ParseInstructionFrom_ForwardTwo_ReturnsCoordinateWithXSetToTwo()
        {
            var coordinate = DiveChallenge.ParseInstructionFrom("forward 2");
            Assert.Equal(2, coordinate.X);
        }

        [Fact]
        public void ParseInstructionFrom_DownFive_ReturnsCoordinateWithYSetToFive()
        {
            var coordinate = DiveChallenge.ParseInstructionFrom("down 5");
            Assert.Equal(5, coordinate.Y);
        }

        [Fact]
        public void ParseInstructionFrom_UpFour_ReturnsCoordinateWithYSetToMinusFour()
        {
            var coordinate = DiveChallenge.ParseInstructionFrom("up 4");
            Assert.Equal(-4, coordinate.Y);
        }

        [Fact]
        public void ShouldBeExpectedXAfterFollowingInstructions()
        {
            var departure = new DiveChallenge.Coordinate(0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(15, arrival.X);
        }

        [Fact]
        public void ShouldBeExpectedYAfterFollowingInstructions()
        {
            var departure = new DiveChallenge.Coordinate(0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(10, arrival.Y);
        }
    }
}
