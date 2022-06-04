namespace AdventOfCode.Challenges
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
                throw new ArgumentException("Cannot parse from an empty string.");
            }

            string[] tokens = text.Split(' ');

            if (tokens.Length != length)
            {
                throw new ArgumentException($"Unexpected number of tokens: {text}.");
            }

            try
            {
                int value = Int32.Parse(tokens[1]);
                if (!validNames.Contains(tokens[0]))
                {
                    throw new ArgumentException($"Unknown instruction: {tokens[0]}");
                }
                return new Instruction(tokens[0], value);
            }
            catch (FormatException e)
            {
                throw new ArgumentException($"Invalid value: {tokens[1]}", e);
            }
        }

        private Instruction(string name, int value)
        {
            Name = name;
            Value = value;
        }

    }
}
