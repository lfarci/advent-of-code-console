using Xunit;
using Moq;
using Moq.Protected;
using System.Linq;
using AdventOfCode.Kit.Console.Web;
using System.Net.Http;
using System.Threading;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using AdventOfCode.Kit.Console.Web.Http;

namespace AdventOfCode.Kit.Console.Tests.Web
{
    public class AdventOfCodeHttpClientTests
    {
        private static readonly string host = "adventofcode.com";
        private static readonly string session = "sessionId";
        private static readonly IAdventOfCodeHttpClient client = new AdventOfCodeHttpClient(host, session);

        [Fact]
        public void Host_ReturnsHostPassedToConstructor() => Assert.Equal(host, client.Host);

        [Fact]
        public void SessionId_ReturnsHostPassedToConstructor() => Assert.Equal(session, client.SessionId);

        [Fact]
        public void BuildHttpGetRequestMessage_ValidResourcePath_ReturnsRequestWithRootUri()
        {
            var request = client.BuildHttpGetRequestMessage("");
            Assert.Equal(request.RequestUri?.ToString(), $"https://{host}/");
        }

        [Theory]
        [InlineData("2021")]
        [InlineData("2020/about")]
        [InlineData("2021/day/1/input")]
        public void BuildHttpGetRequestMessage_ValidResourcePath_ReturnsRequestWithExpectedUri(string resourcePath)
        {
            var request = client.BuildHttpGetRequestMessage(resourcePath);
            Assert.Equal(request.RequestUri?.ToString(), $"https://{host}/{resourcePath}");
        }

        [Fact]
        public void BuildHttpGetRequestMessage_ValidResourcePath_ReturnsRequestWithHostHeader()
        {
            Assert.True(client.BuildHttpGetRequestMessage("/").Headers.Contains("Host"));
        }

        [Fact]
        public void BuildHttpGetRequestMessage_ValidResourcePath_ReturnsRequestWithExpectedHostValue()
        {
            var request = client.BuildHttpGetRequestMessage("/");
            Assert.Equal(host, request.Headers.Host);
        }

        [Fact]
        public void BuildHttpGetRequestMessage_ValidResourcePath_ReturnsRequestWithSingleCookieHeader()
        {
            var request = client.BuildHttpGetRequestMessage("/");
            Assert.Single(request.Headers.GetValues("Cookie").ToList());
        }

        [Fact]
        public void BuildHttpGetRequestMessage_ValidResourcePath_ReturnsRequestWithSessionIdInCookies()
        {
            var request = client.BuildHttpGetRequestMessage("/");
            var cookies = request.Headers.GetValues("Cookie").ToList();
            Assert.Contains($"session={session}", cookies);
        }

        [Fact]
        public void GetResourceAsync_SuccessStatusCode_ReturnsHttpResponseMessage()
        {
            var httpClientHandlerMock = new Mock<HttpClientHandler>();

            httpClientHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            var client = new AdventOfCodeHttpClient(httpClientHandlerMock.Object, host, session);
            Assert.NotNull(client.GetResourceAsync("/"));
        }

        [Fact]
        public async Task GetResourceAsync_HttpRequestExceptionThrown_ThrowsIOException()
        {
            var httpClientHandlerMock = new Mock<HttpClientHandler>();

            httpClientHandlerMock
                .Protected()
                .Setup("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws<HttpRequestException>();

            var client = new AdventOfCodeHttpClient(httpClientHandlerMock.Object, host, session);
            await Assert.ThrowsAsync<IOException>(() => client.GetResourceAsync("/"));
        }
    }
}
