using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Challenges
{
    public class DiveChallenge : DailyChallenge
    {

        public struct Coordinate
        {
            private readonly int x;
            public int X { get { return x; } }

            private readonly int y;
            public int Y { get { return y; } }

            public Coordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Coordinate Apply(Coordinate move)
            {
                return new Coordinate(x + move.x, y + move.y);
            }
        }

        private static int instructionLength = 2;

        private static IDictionary<string, Coordinate> instructionsSet = new Dictionary<string, Coordinate>()
        {
            { "forward", new Coordinate(1, 0) },
            { "down", new Coordinate(0, 1) },
            { "up", new Coordinate(0, -1) },
        };

        private const string title = "Dive!";
        private const int day = 2;

        public DiveChallenge() : base(title, day)
        {
        }

        public static Coordinate ParseInstructionFrom(string instruction)
        {
            if (string.IsNullOrEmpty(instruction))
            {
                throw new ArgumentException(nameof(instruction));
            }

            var coordinate = new Coordinate(0, 0);
            string[] tokens = instruction.Split(' ');

            if (tokens.Length != instructionLength)
            {
                throw new ArgumentException(nameof(instruction));
            }

            try
            {
                int factor = Int32.Parse(tokens[1]);

                if (!instructionsSet.ContainsKey(tokens[0]))
                { 
                    throw new ArgumentException(nameof(instruction));
                }

                Coordinate move = instructionsSet[tokens[0]];
                coordinate = new Coordinate(move.X * factor, move.Y * factor);

            }
            catch (FormatException e)
            {
                throw new ArgumentException(nameof(instruction), e);
            }

            return coordinate;
        }

        public static Coordinate FollowInstructionsFrom(Coordinate coordinate, string[] instructions)
        {
            Coordinate current = coordinate;
            foreach (string instruction in instructions)
            {
                Coordinate move = ParseInstructionFrom(instruction);
                current = current.Apply(move);
            }
            return current;
        }

        protected override void Run(string[] lines)
        {
            Coordinate coordinate = new Coordinate(0, 0);
            coordinate = FollowInstructionsFrom(coordinate, lines);
            var result = coordinate.X * coordinate.Y;
            RegisterResultEntry($"puzzle answer is {result}");
        }

    }
}
