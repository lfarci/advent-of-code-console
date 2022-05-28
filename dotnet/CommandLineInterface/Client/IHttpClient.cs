namespace CommandLineInterface.Client
{
    public interface IHttpClient
    {
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
