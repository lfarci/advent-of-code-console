namespace AdventOfCode.Kit.Client.Web
{
    public interface IConfiguration
    {
        public string Host { get; init; }
        public string SessionId { get; init; }
    }
}
