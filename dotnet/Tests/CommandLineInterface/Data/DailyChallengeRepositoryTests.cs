using AdventOfCode2021.Challenges;
using AdventOfCode2021.CommandLineInterface.Data;
using Xunit;

namespace Tests.CommandLineInterface.Data
{
    public class DailyChallengeRepositoryTests
    {
        private static readonly IChallengeRepository repository = ChallengeRepository.Instance;

        [Fact]
        public void Challenges_IsNotEmpty()
        {
            Assert.NotEmpty(repository.Challenges);
        }

        [Fact]
        public void FindByDay_NegativeInteger_ReturnsNull()
        {
            Assert.Null(repository.FindByDay(-1));
        }

        [Fact]
        public void FindByDay_UnknownIntegerDay_ReturnsNull()
        {
            Assert.Null(repository.FindByDay(26));
        }

        [Fact]
        public void FindByDay_ExistingIntegerDay_ReturnsDailyChallenge()
        {
            Assert.NotNull(repository.FindByDay(1));
        }

        [Fact]
        public void FindByDay_SonarSweepIntegerDay_ReturnsDailyChallenge()
        {
            Assert.IsType<SonarSweepChallenge>(repository.FindByDay(1));
        }

        [Fact]
        public void FindByDay_EmptyString_ReturnsNull()
        {
            Assert.Null(repository.FindByDay(""));
        }

        [Fact]
        public void FindByDay_NegativeDayString_ReturnsNull()
        {
            Assert.Null(repository.FindByDay("-1"));
        }

        [Fact]
        public void FindByDay_UnknownDayString_ReturnsNull()
        {
            Assert.Null(repository.FindByDay("26"));
        }

        [Fact]
        public void FindByDay_ExistingDayString_ReturnsDailyChallenge()
        {
            Assert.NotNull(repository.FindByDay("1"));
        }

        [Fact]
        public void FindByDay_SonarSweeStringDay_ReturnsDailyChallenge()
        {
            Assert.IsType<SonarSweepChallenge>(repository.FindByDay("1"));
        }
    }
}
