using AdventOfCode2021.CommandLineInterface.Data;
using Moq;
using System;
using System.IO;
using Xunit;

namespace Tests.CommandLineInterface.Data
{
    public class PuzzleInputRepositoryTests
    {
        private static readonly int defaultYear = 2021;
        private static readonly int defaultDay = 1;

        [Fact]
        public void Instance_ReturnsSingleton()
        {
            Assert.Equal(PuzzleInputRepository.Instance, PuzzleInputRepository.Instance);
        }

        [Fact]
        public async void FindByYearAndDayAsync_ClientThrowsException_ThrowsOutOfRangeException()
        {
            var repository = GetRepositoryThatThrows<IOException>();
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await repository.FindByYearAndDayAsync(defaultYear, defaultDay);
            });
        }

        [Fact]
        public async void FindByYearAndDayAsync_ClientThrowsException_ThrowExceptionExpectedMessage()
        {
            var repository = GetRepositoryThatThrows<IOException>();
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await repository.FindByYearAndDayAsync(defaultYear, defaultDay);
            });

            Assert.Equal(PuzzleInputRepository.GetNotFoundErrorMessage(defaultYear, defaultDay), thrown.Message);
        }

        [Fact]
        public async void FindByYearAndDayAsync_EmptyFile_ReturnsEmptyArray()
        {
            var repository = GetRepositoryThatReturns("");
            string[] lines = await repository.FindByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Empty(lines);
        }

        [Fact]
        public async void FindByYearAndDayAsync_ThreeLines_ReturnsArrayWithExpectedSize()
        {
            var repository = GetRepositoryThatReturns("line1\nline2\nline3");
            string[] lines = await repository.FindByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Equal(3, lines.Length);
        }

        [Fact]
        public async void FindByYearAndDayAsync_ThreeLines_ReturnsExpectedLines()
        {
            var repository = GetRepositoryThatReturns("line1\nline2\nline3");
            string[] lines = await repository.FindByYearAndDayAsync(defaultYear, defaultDay);
            Assert.Contains("line1", lines);
            Assert.Contains("line2", lines);
            Assert.Contains("line3", lines);
        }

        private static IPuzzleInputRepository GetRepositoryThatThrows<TException>() where TException : Exception, new()
        {
            var client = Helpers.GetClientThatThrows<TException>(c => c.GetPuzzleInputAsStreamAsync(defaultYear, defaultDay));
            return new PuzzleInputRepository(client);
        }

        private static IPuzzleInputRepository GetRepositoryThatReturns(string result)
        {
            var client = Helpers.GetClientThatReturns(result, c => c.GetPuzzleInputAsStreamAsync(defaultYear, defaultDay).Result);
            return new PuzzleInputRepository(client);
        }

    }
}
