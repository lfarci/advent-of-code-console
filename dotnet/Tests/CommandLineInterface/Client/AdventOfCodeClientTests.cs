using AdventOfCode2021.CommandLineInterface.Client;
using Moq;
using System.IO;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Tests.CommandLineInterface.Client
{
    public class AdventOfCodeClientTests
    {
        private static readonly Mock<IAdventOfCodeHttpClient> clientMock = new Mock<IAdventOfCodeHttpClient>();

        private static IAdventOfCodeClient GetClientThatReturns(HttpResponseMessage? response)
        {
            clientMock
                .Setup(client => client.GetResourceAsync(It.IsAny<string>()).Result)
                .Returns(response);
            return new AdventOfCodeClient(clientMock.Object);
        }

        [Fact]
        public void GetDailyChallengeInputAsStreamAsync_ResponseIsNull_ThrowsClientException()
        {
            var client = GetClientThatReturns(null);
            Assert.ThrowsAsync<AdventOfCodeClientException>(() => client.GetDailyChallengeInputAsStreamAsync(2021, 1));
        }

        [Fact]
        public void GetDailyChallengeInputAsStreamAsync_NoSuccessStatusCode_ThrowsClientException()
        {
            var client = GetClientThatReturns(new HttpResponseMessage {
                StatusCode = HttpStatusCode.BadRequest
            });
            Assert.ThrowsAsync<AdventOfCodeClientException>(() => client.GetDailyChallengeInputAsStreamAsync(2021, 1));
        }

        [Fact]
        public async void GetDailyChallengeInputAsStreamAsync_SuccessStatusCode_ReturnsContentAsStream()
        {
            string expectedInput = "This is my content";
            var client = GetClientThatReturns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedInput)
            });
            var input = await client.GetDailyChallengeInputAsStreamAsync(2021, 1);
            Assert.Equal(expectedInput, await new StreamReader(input).ReadToEndAsync());
        }

    }
}
