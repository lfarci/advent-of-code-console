namespace AdventOfCode.Kit.Client.Web.Http
{
    internal interface IHttpRequestSender
    {
        /// <summary>
        /// Send a HTTP GET request to the given resource path.
        /// 
        /// Targeted host and headers should be defined by implementations of this
        /// interface.
        /// </summary>
        /// <param name="resourcePath">The path to the resource you want to get.</param>
        /// <returns>
        /// A HTTP response.
        /// </returns>
        /// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
        public Task<HttpResponseMessage?> GetResourceAsync(string resourcePath);
    }
}
