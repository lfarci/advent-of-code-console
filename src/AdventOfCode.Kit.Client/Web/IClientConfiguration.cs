namespace AdventOfCode.Kit.Client
{
    internal interface IClientConfiguration
    {
        public string Host { get; init; }
        public string SessionId { get; init; }
    }
}
