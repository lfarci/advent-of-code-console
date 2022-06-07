using AdventOfCode.Challenges;
using Xunit;

namespace Tests.Challenges.Day02
{
    public class CoordinateTests
    {
        [Fact]
        public void Apply_ForwardTwo_ReturnsCoordinateWithXIncrementedByTwo()
        {
            ICoordinate coordinate = new Coordinate(3, -1);
            coordinate = coordinate.Apply(Instruction.Parse("forward 2"));
            Assert.Equal(5, coordinate.X);
        }

        [Fact]
        public void Apply_DownFour_ReturnsCoordinateWithYIncrementedByFour()
        {
            ICoordinate coordinate = new Coordinate(3, 100);
            coordinate = coordinate.Apply(Instruction.Parse("down 4"));
            Assert.Equal(104, coordinate.Y);
        }


        [Fact]
        public void Apply_UpThree_ReturnsCoordinateWithYDecrementedByThree()
        {
            ICoordinate coordinate = new Coordinate(3, 100);
            coordinate = coordinate.Apply(Instruction.Parse("up 3"));
            Assert.Equal(97, coordinate.Y);
        }

    }
}
