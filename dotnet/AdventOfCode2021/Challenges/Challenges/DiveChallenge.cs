using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Challenges
{
    public class DiveChallenge : DailyChallenge
    {

        public interface ICoordinate
        {
            public int X { get; }
            public int Y { get; }
            ICoordinate Apply(Instruction instruction);
        }

        public struct Instruction
        {
            private static int length = 2;
            private static string[] validNames = {"forward", "down", "up"};

            public string Name { get; }
            public int Value { get; }

            public static Instruction Parse(string text)
            {
                if (string.IsNullOrEmpty(text))
                {
                    throw new ArgumentException(nameof(text));
                }

                var coordinate = new Coordinate(0, 0);
                string[] tokens = text.Split(' ');

                if (tokens.Length != length)
                {
                    throw new ArgumentException(nameof(text));
                }

                try
                {
                    int value = Int32.Parse(tokens[1]);
                    if (!validNames.Contains(tokens[0]))
                    {
                        throw new ArgumentException(nameof(text));
                    }
                    return new Instruction(tokens[0], value);
                }
                catch (FormatException e)
                {
                    throw new ArgumentException(nameof(text), e);
                }
            }

            private Instruction(string name, int value)
            {
                Name = name;
                Value = value;
            }

        }

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

        private const string title = "Dive!";
        private const int day = 2;

        public DiveChallenge() : base(title, day)
        {
        }

        public static ICoordinate FollowInstructionsFrom(ICoordinate coordinate, string[] lines)
        {
            ICoordinate current = coordinate;
            foreach (string line in lines)
            {
                Instruction instruction = Instruction.Parse(line);
                current = current.Apply(instruction);
            }
            return current;
        }

        protected override void Run(string[] lines)
        {
            ICoordinate coordinate = new Coordinate(0, 0);
            ICoordinate submarineCoordinate = new SubmarineCoordinate(0, 0, 0);
            coordinate = FollowInstructionsFrom(coordinate, lines);
            submarineCoordinate = FollowInstructionsFrom(submarineCoordinate, lines);
            RegisterResultEntry($"puzzle answer is {coordinate.X * coordinate.Y}");
            RegisterResultEntry($"puzzle answer is {submarineCoordinate.X * submarineCoordinate.Y}");
        }

    }
}
