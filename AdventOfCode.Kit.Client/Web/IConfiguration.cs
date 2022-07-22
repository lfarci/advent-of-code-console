namespace AdventOfCode.Kit.Client.Web
{
    internal interface IConfiguration
    {
        public string Host { get; init; }
        public string SessionId { get; init; }
    }
}
