namespace AdventOfCode2021.Challenges.Day02
{
    public interface ICoordinate
    {
        public int X { get; }
        public int Y { get; }
        ICoordinate Apply(Instruction instruction);
    }
}
