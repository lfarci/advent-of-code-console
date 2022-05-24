using AdventOfCode2021.Challenges;
using Xunit;

namespace AdventOfCode2021Tests.Challenges
{
    public class SonarSweepTests
    {
        private static string[] depths = { "199", "200", "208", "210", "200", "207", "240", "269", "260", "263" };


        [Fact]
        public void CountDepthIncrements()
        {
            Assert.Equal(7, SonarSweepChallenge.CountDepthIncrements(depths));
        }

        [Fact]
        public void CountDepthIncrementsWithWindowOfThree()
        {
            Assert.Equal(5, SonarSweepChallenge.CountDepthIncrements(depths, 3));
        }
    }
}