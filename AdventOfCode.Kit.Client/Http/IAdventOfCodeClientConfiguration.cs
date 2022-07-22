namespace AdventOfCode.Kit.Client.Http
{
    internal interface IAdventOfCodeClientConfiguration
    {
        public string Host { get; init; }
        public string SessionId { get; init; }
    }
}
