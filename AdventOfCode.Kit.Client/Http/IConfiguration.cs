namespace AdventOfCode.Kit.Client.Http
{
    internal interface IConfiguration
    {
        public string Host { get; init; }
        public string SessionId { get; init; }
    }
}
