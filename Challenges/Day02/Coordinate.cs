namespace AdventOfCode.Challenges
{
    public class Coordinate : ICoordinate
    {
        private static IDictionary<string, ICoordinate> directions = new Dictionary<string, ICoordinate>()
            {
                { "forward", new Coordinate(1, 0) },
                { "down", new Coordinate(0, 1) },
                { "up", new Coordinate(0, -1) },
            };

        protected readonly int x;
        public int X { get { return x; } }

        protected readonly int y;
        public int Y { get { return y; } }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public virtual ICoordinate Apply(Instruction instruction)
        {
            var direction = directions[instruction.Name];
            ICoordinate move = new Coordinate(direction.X * instruction.Value, direction.Y * instruction.Value);
            return Apply(move);
        }

        private ICoordinate Apply(ICoordinate move)
        {
            return new Coordinate(x + move.X, y + move.Y);
        }

        public override bool Equals(object? obj)
        {
            return obj is Coordinate coordinate &&
                   x == coordinate.x &&
                   y == coordinate.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
    }

}
