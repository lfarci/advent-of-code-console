using Xunit;
using AdventOfCode2021.Challenges;
using System;

namespace Tests.Challenges
{

    public class InstructionTests
    {
        [Fact]
        public void Parse_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.Instruction.Parse(""));
        }

        [Fact]
        public void Parse_InvalidInstruction_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.Instruction.Parse("3 back"));
        }


        [Fact]
        public void Parse_UnknownInstruction_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.Instruction.Parse("unknown 3"));
        }

        [Fact]
        public void Parse_SingleToken_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.Instruction.Parse("forward"));
        }

        [Fact]
        public void Parse_ThreeTokens_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => DiveChallenge.Instruction.Parse("forward 2 fast"));
        }

        [Fact]
        public void Parse_ForwardTwo_ReturnsExpectedInstruction()
        {
            var instruction = DiveChallenge.Instruction.Parse("forward 2");
            Assert.Equal("forward", instruction.Name);
            Assert.Equal(2, instruction.Value);
        }

        [Fact]
        public void Parse_DownFive_ReturnsExpectedInstruction()
        {
            var instruction = DiveChallenge.Instruction.Parse("down 5");
            Assert.Equal("down", instruction.Name);
            Assert.Equal(5, instruction.Value);
        }

        [Fact]
        public void Parse_UpFour_ReturnsExpectedInstruction()
        {
            var instruction = DiveChallenge.Instruction.Parse("up 4");
            Assert.Equal("up", instruction.Name);
            Assert.Equal(4, instruction.Value);
        }
    }

    public class CoordinateTests
    {
        [Fact]
        public void Apply_ForwardTwo_ReturnsCoordinateWithXIncrementedByTwo()
        {
            DiveChallenge.ICoordinate coordinate = new DiveChallenge.Coordinate(3, -1);
            coordinate = coordinate.Apply(DiveChallenge.Instruction.Parse("forward 2"));
            Assert.Equal(5, coordinate.X);
        }

        [Fact]
        public void Apply_DownFour_ReturnsCoordinateWithYIncrementedByFour()
        {
            DiveChallenge.ICoordinate coordinate = new DiveChallenge.Coordinate(3, 100);
            coordinate = coordinate.Apply(DiveChallenge.Instruction.Parse("down 4"));
            Assert.Equal(104, coordinate.Y);
        }


        [Fact]
        public void Apply_UpThree_ReturnsCoordinateWithYDecrementedByThree()
        {
            DiveChallenge.ICoordinate coordinate = new DiveChallenge.Coordinate(3, 100);
            coordinate = coordinate.Apply(DiveChallenge.Instruction.Parse("up 3"));
            Assert.Equal(97, coordinate.Y);
        }

    }

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
            var departure = new DiveChallenge.Coordinate(0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(15, arrival.X);
        }

        [Fact]
        public void FollowInstructionsFrom_CoordinateAndValidInstructions_RetunsCoordinateWithExpectedY()
        {
            var departure = new DiveChallenge.Coordinate(0, 0);
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
            var departure = new DiveChallenge.Coordinate(x, y);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, new string[0]);
            Assert.True(departure.Equals(arrival));
        }

        [Fact]
        public void FollowInstructionsFrom_ValidInstructions_RetunsCoordinateWithExpectedX()
        {
            var departure = new DiveChallenge.SubmarineCoordinate(0, 0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(15, arrival.X);
        }

        [Fact]
        public void FollowInstructionsFrom_ValidInstructions_RetunsCoordinateWithExpectedY()
        {
            var departure = new DiveChallenge.SubmarineCoordinate(0, 0, 0);
            var arrival = DiveChallenge.FollowInstructionsFrom(departure, instructions);
            Assert.Equal(60, arrival.Y);
        }
    }
}
