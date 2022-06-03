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
            var repository = GetRepositoryThatThrows<IOException>();
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            });
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_ClientThrowsException_ThrowExceptionExpectedMessage()
        {
            var repository = GetRepositoryThatThrows<IOException>();
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            });

            Assert.Equal(ChallengeInputRepository.GetNotFoundErrorMessage(defaultYear, defaultDay), thrown.Message);
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_EmptyFile_ReturnsEmptyArray()
        {
            var repository = GetRepositoryThatReturns("");
            string[] lines = await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Empty(lines);
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_ThreeLines_ReturnsArrayWithExpectedSize()
        {
            var repository = GetRepositoryThatReturns("line1\nline2\nline3");
            string[] lines = await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Equal(3, lines.Length);
        }

        [Fact]
        public async void FindInputLinesByYearAndDayAsync_ThreeLines_ReturnsExpectedLines()
        {
            var repository = GetRepositoryThatReturns("line1\nline2\nline3");
            string[] lines = await repository.FindInputLinesByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Contains("line1", lines);
            Assert.Contains("line2", lines);
            Assert.Contains("line3", lines);
        }

        private static IChallengeInputRepository GetRepositoryThatThrows<TException>() where TException : Exception, new()
        {
            var client = Helpers.GetClientThatThrows<TException>(c => c.GetPuzzleInputAsStreamAsync(defaultYear, defaultDay));
            return new ChallengeInputRepository(client);
        }

        private static IChallengeInputRepository GetRepositoryThatReturns(string result)
        {
            var client = Helpers.GetClientThatReturns(result, c => c.GetPuzzleInputAsStreamAsync(defaultYear, defaultDay).Result);
            return new ChallengeInputRepository(client);
        }

    }
}
