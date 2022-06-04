using AdventOfCode.Challenges;
using System;
using Xunit;

namespace Tests.Challenges.Day02
{
    public class InstructionTests
    {
        [Fact]
        public void Parse_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Instruction.Parse(""));
        }

        [Fact]
        public void Parse_InvalidInstruction_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Instruction.Parse("3 back"));
        }

        [Fact]
        public void Parse_UnknownInstruction_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Instruction.Parse("unknown 3"));
        }

        [Fact]
        public void Parse_SingleToken_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Instruction.Parse("forward"));
        }

        [Fact]
        public void Parse_ThreeTokens_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Instruction.Parse("forward 2 fast"));
        }

        [Fact]
        public void Parse_ForwardTwo_ReturnsExpectedInstruction()
        {
            var instruction = Instruction.Parse("forward 2");
            Assert.Equal("forward", instruction.Name);
            Assert.Equal(2, instruction.Value);
        }

        [Fact]
        public void Parse_DownFive_ReturnsExpectedInstruction()
        {
            var instruction = Instruction.Parse("down 5");
            Assert.Equal("down", instruction.Name);
            Assert.Equal(5, instruction.Value);
        }

        [Fact]
        public void Parse_UpFour_ReturnsExpectedInstruction()
        {
            var instruction = Instruction.Parse("up 4");
            Assert.Equal("up", instruction.Name);
            Assert.Equal(4, instruction.Value);
        }
    }
}
