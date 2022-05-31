using AdventOfCode2021.CommandLineInterface.WebClient;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tests.CommandLineInterface.WebClient
{
    public class AdventOfCodeClientTests
    {
        private static readonly Mock<IAdventOfCodeHttpClient> clientMock = new();

        [Fact]
        public void Instance_AlwaysReturnsSameInstance()
        {
            Assert.Equal(AdventOfCodeClient.Instance, AdventOfCodeClient.Instance);
        }

        [Fact]
        public void GetDayPageAsStreamAsync_ResponseIsNull_ThrowsClientException()
        {
            AssertThrowsClientException(null, c => c.GetDayPageAsStreamAsync(2021, 1));
        }

        [Fact]
        public void GetDayPageAsStreamAsync_NoSuccessStatusCode_ThrowsClientException()
        {
            AssertThrowsClientException(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            }, c => c.GetDayPageAsStreamAsync(2021, 1));
        }

        [Fact]
        public async void GetDayPageAsStreamAsync_SuccessStatusCode_ReturnsContentAsStream()
        {
            await AssertReturnsContent("Day page", c => c.GetDayPageAsStreamAsync(2021, 1));
        }

        [Fact]
        public void GetCalendarPageAsStreamAsync_ResponseIsNull_ThrowsClientException()
        {
            AssertThrowsClientException(null, c => c.GetCalendarPageAsStreamAsync(2021));
        }

        [Fact]
        public void GetCalendarPageAsStreamAsync_NoSuccessStatusCode_ThrowsClientException()
        {
            AssertThrowsClientException(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            }, c => c.GetCalendarPageAsStreamAsync(2021));
        }

        [Fact]
        public async void GetCalendarPageAsStreamAsync_SuccessStatusCode_ReturnsContentAsStream()
        {
            await AssertReturnsContent("Calendar page", c => c.GetCalendarPageAsStreamAsync(2021));

        }

        [Fact]
        public void GetPuzzleInputAsStreamAsync_ResponseIsNull_ThrowsClientException()
        {
            AssertThrowsClientException(null, c => c.GetPuzzleInputAsStreamAsync(2021, 1));
        }

        [Fact]
        public void GetPuzzleInputAsStreamAsync_NoSuccessStatusCode_ThrowsClientException()
        {
            AssertThrowsClientException(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            }, c => c.GetPuzzleInputAsStreamAsync(2021, 1));
        }

        [Fact]
        public async void GetPuzzleInputAsStreamAsync_SuccessStatusCode_ReturnsContentAsStream()
        {
            await AssertReturnsContent("Puzzle input", c => c.GetPuzzleInputAsStreamAsync(2021, 1));
        }

        private static IAdventOfCodeClient GetClientThatReturns(HttpResponseMessage? response)
        {
            clientMock
                .Setup(client => client.GetResourceAsync(It.IsAny<string>()).Result)
                .Returns(response);
            return new AdventOfCodeClient(clientMock.Object);
        }

        private static void AssertThrowsClientException(HttpResponseMessage? message, Func<IAdventOfCodeClient, Task> call)
        {
            var client = GetClientThatReturns(message);
            Assert.ThrowsAsync<AdventOfCodeClientException>(() => call(client));
        }
        private static async Task AssertReturnsContent(string content, Func<IAdventOfCodeClient, Task<Stream>> call)
        {
            var client = GetClientThatReturns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            });
            var input = await call(client);
            Assert.Equal(content, await new StreamReader(input).ReadToEndAsync());
        }

    }
}
