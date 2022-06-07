namespace AdventOfCode.Console.Web
{
    internal interface IAdventOfCodeHttpClient
    {
        public string Host { get; }
        public string SessionId { get; }
        public HttpRequestMessage BuildHttpGetRequestMessage(string resourcePath);
        public Task<HttpResponseMessage?> GetResourceAsync(string resourcePath);
    }
}
