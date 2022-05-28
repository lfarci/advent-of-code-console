namespace AdventOfCode2021.CommandLineInterface.Client
{
    public class AdventOfCodeClientException : Exception
    {
        public AdventOfCodeClientException()
        {
        }

        public AdventOfCodeClientException(string? message) : base(message)
        {
        }

        public AdventOfCodeClientException(string? message, Exception cause) : base(message, cause)
        {
        }
    }
}
