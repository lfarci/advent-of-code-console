namespace AdventOfCode2021.Challenges.Day02
{
    public struct Instruction
    {
        private static int length = 2;
        private static string[] validNames = { "forward", "down", "up" };

        public string Name { get; }
        public int Value { get; }

        public static Instruction Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(nameof(text));
            }

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
}
