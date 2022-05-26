using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Challenges.Day02
{
    public partial class DiveChallenge : DailyChallenge
    {
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
