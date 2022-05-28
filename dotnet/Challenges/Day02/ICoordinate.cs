namespace AdventOfCode2021.Challenges
{
    public interface ICoordinate
    {
        public int X { get; }
        public int Y { get; }
        ICoordinate Apply(Instruction instruction);
    }
}
