using AdventOfCode2021.CommandLineInterface.Client;
using AdventOfCode2021.CommandLineInterface.Data;
using Moq;
using System;
using System.IO;
using Xunit;

namespace Tests.CommandLineInterface.Data
{
    public class ChallengeInputRepositoryTests
    {
        private static readonly int defaultYear = 2021;
        private static readonly int defaultDay = 1;

        [Fact]
        public void Instance_ReturnsSingleton()
        {
            Assert.Equal(ChallengeInputRepository.Instance, ChallengeInputRepository.Instance);
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_ClientThrowsException_ThrowsOutOfRangeException()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await ThrowingRepositoryMock.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            });
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_ClientThrowsException_ThrowExceptionExpectedMessage()
        {
            var e = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await ThrowingRepositoryMock.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            });

            Assert.Equal($"Could not find lines for year 2021 and day 1.", e.Message);
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_EmptyFile_ReturnsEmptyArray()
        {
            var repository = GetRepositoryMockWithInput("");
            string[] lines = await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Empty(lines);
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_ThreeLines_ReturnsArrayWithExpectedSize()
        {
            var repository = GetRepositoryMockWithInput("line1\nline2\nline3");
            string[] lines = await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Equal(3, lines.Length);
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_ThreeLines_ReturnsExpectedLines()
        {
            var repository = GetRepositoryMockWithInput("line1\nline2\nline3");
            string[] lines = await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Contains("line1", lines);
            Assert.Contains("line2", lines);
            Assert.Contains("line3", lines);
        }

        private static ChallengeInputRepository ThrowingRepositoryMock
        {
            get
            {
                var repositoryMock = new Mock<ChallengeInputRepository>();
                repositoryMock.CallBase = true;

                repositoryMock
                    .Setup(r => r.FindInputStreamByYearAndDayAsync(defaultYear, defaultDay).Result)
                    .Throws<AdventOfCodeClientException>();

                return repositoryMock.Object;
            }
        }

        private static ChallengeInputRepository GetRepositoryMockWithInput(string input)
        {
            var repositoryMock = new Mock<ChallengeInputRepository>();
            repositoryMock.CallBase = true;

            repositoryMock
                .Setup(r => r.FindInputStreamByYearAndDayAsync(defaultYear, defaultDay).Result)
                .Returns(GenerateStreamFromString(input));

            return repositoryMock.Object;
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

    }
}
