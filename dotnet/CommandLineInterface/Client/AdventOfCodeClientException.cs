namespace AdventOfCode2021
{
    public class AdventOfCodeClientException : Exception
    {
        public AdventOfCodeClientException()
        {
        }

        public AdventOfCodeClientException(string? message) : base(message)
        {
        }
    }
}
