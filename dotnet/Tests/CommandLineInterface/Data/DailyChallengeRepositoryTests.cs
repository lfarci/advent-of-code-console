using AdventOfCode2021.Challenges;
using AdventOfCode2021.CommandLineInterface.Data;
using Xunit;

namespace Tests.CommandLineInterface.Data
{
    public class DailyChallengeRepositoryTests
    {
        [Fact]
        public void Challenges_IsNotEmpty()
        {
            Assert.NotEmpty(DailyChallengeRepository.Challenges);
        }

        [Fact]
        public void FindByDay_NegativeInteger_ReturnsNull()
        {
            Assert.Null(DailyChallengeRepository.FindByDay(-1));
        }

        [Fact]
        public void FindByDay_UnknownIntegerDay_ReturnsNull()
        {
            Assert.Null(DailyChallengeRepository.FindByDay(26));
        }

        [Fact]
        public void FindByDay_ExistingIntegerDay_ReturnsDailyChallenge()
        {
            Assert.NotNull(DailyChallengeRepository.FindByDay(1));
        }

        [Fact]
        public void FindByDay_SonarSweepIntegerDay_ReturnsDailyChallenge()
        {
            Assert.IsType<SonarSweepChallenge>(DailyChallengeRepository.FindByDay(1));
        }

        [Fact]
        public void FindByDay_EmptyString_ReturnsNull()
        {
            Assert.Null(DailyChallengeRepository.FindByDay(""));
        }

        [Fact]
        public void FindByDay_NegativeDayString_ReturnsNull()
        {
            Assert.Null(DailyChallengeRepository.FindByDay("-1"));
        }

        [Fact]
        public void FindByDay_UnknownDayString_ReturnsNull()
        {
            Assert.Null(DailyChallengeRepository.FindByDay("26"));
        }

        [Fact]
        public void FindByDay_ExistingDayString_ReturnsDailyChallenge()
        {
            Assert.NotNull(DailyChallengeRepository.FindByDay("1"));
        }

        [Fact]
        public void FindByDay_SonarSweeStringDay_ReturnsDailyChallenge()
        {
            Assert.IsType<SonarSweepChallenge>(DailyChallengeRepository.FindByDay("1"));
        }
    }
}
