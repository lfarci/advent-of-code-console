﻿using AdventOfCode.Kit.Client.Web.Resources;
using AdventOfCodeConsole.Tests.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Web.Resources
{
    public class DayPageRepositoryTests
    {
        private static readonly int defaultYear = 2021;
        private static readonly int defaultDay = 1;

        [Fact]
        public async Task FindByYearAndDayAsync_ClientThrowsIOException_ThrowsIOException()
        {
            var repository = GetRepositoryThatThrows<IOException>();
            var thrown = await Assert.ThrowsAsync<IOException>(async () =>
            {
                await repository.FindByYearAndDayAsync(defaultYear, defaultDay);
            });
            Assert.Equal(DayPageRepository.GetNotFoundErrorMessage(defaultYear, defaultDay), thrown.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Invalid page")]
        [InlineData("<body><main></main></body>")]
        public async Task FindByYearAndDayAsync_ParserThrowsFormatException_ThrowsInvalidOperation(string invalidPage)
        {
            var repository = GetRepositoryThatReturns(invalidPage);
            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await repository.FindByYearAndDayAsync(defaultYear, defaultDay);
            });
            Assert.Equal(DayPageRepository.GetParseErrorMessage(defaultYear, defaultDay), thrown.Message);
        }

        [Theory]
        [InlineData("AdventOfCode.Kit.Client.Tests.Fixtures.NotStartedDayPage.html")]
        [InlineData("AdventOfCode.Kit.Client.Tests.Fixtures.CompleteDayPage.html")]
        [InlineData("AdventOfCode.Kit.Client.Tests.Fixtures.VeryCompleteDayPage.html")]
        public async Task FindByYearAndDayAsync_ValidPage_ReturnsDay(string resourceName)
        {
            var content = Helpers.ReadResourceContentAsString(resourceName);
            var repository = GetRepositoryThatReturns(content);
            Assert.NotNull(await repository.FindByYearAndDayAsync(defaultYear, defaultDay));
        }

        [Theory]
        [InlineData(12, "AdventOfCode.Kit.Client.Tests.Fixtures.NotStartedDayPage.html")]
        [InlineData(4, "AdventOfCode.Kit.Client.Tests.Fixtures.CompleteDayPage.html")]
        [InlineData(7, "AdventOfCode.Kit.Client.Tests.Fixtures.VeryCompleteDayPage.html")]
        public async Task FindByYearAndDayAsync_ValidPage_ReturnsDayWithIndex(int index, string resourceName)
        {
            var content = Helpers.ReadResourceContentAsString(resourceName);
            var repository = GetRepositoryThatReturns(content, index);
            var page = await repository.FindByYearAndDayAsync(defaultYear, index);
            Assert.Equal(index, page.Index);
        }

        private static IDayPageRepository GetRepositoryThatThrows<TException>() where TException : Exception, new()
        {
            var client = Helpers.GetClientThatThrows<TException>(c => c.GetDayPageAsStreamAsync(defaultYear, defaultDay));
            return new DayPageRepository(client);
        }

        private static IDayPageRepository GetRepositoryThatReturns(string result)
        {
            return GetRepositoryThatReturns(result, defaultDay);
        }

        private static IDayPageRepository GetRepositoryThatReturns(string result, int day)
        {
            var client = Helpers.GetClientThatReturns(result, c => c.GetDayPageAsStreamAsync(defaultYear, day).Result);
            return new DayPageRepository(client);
        }
    }
}
