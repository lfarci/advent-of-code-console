namespace AdventOfCode.Challenges
{
    public class SubmarineCoordinate : Coordinate
    {
        private static IDictionary<string, SubmarineCoordinate> directions = new Dictionary<string, SubmarineCoordinate>()
            {
                { "forward", new SubmarineCoordinate(1, 0, 0) },
                { "down", new SubmarineCoordinate(0, 0, 1) },
                { "up", new SubmarineCoordinate(0, 0, -1) },
            };

        private readonly int aim;
        public int Aim { get { return aim; } }

        public SubmarineCoordinate(int x, int y, int aim) : base(x, y)
        {
            this.aim = aim;
        }

        public override ICoordinate Apply(Instruction instruction)
        {
            var direction = directions[instruction.Name];
            return Apply(new SubmarineCoordinate(
                direction.X * instruction.Value,
                direction.Y * instruction.Value,
                direction.Aim * instruction.Value
            ));
        }

        private SubmarineCoordinate Apply(SubmarineCoordinate move)
        {
            var depthMove = move.y;
            if (move.x != 0)
            {
                depthMove = aim * move.x;
            }
            return new SubmarineCoordinate(x + move.x, y + depthMove, aim + move.aim);
        }
    }

}
