using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Challenges.Day01
{
    public class SonarSweepChallenge : DailyChallenge
    {

        private const string title = "Sonar Sweep";
        private const int day = 1;

        public SonarSweepChallenge() : base(title, day)
        {
        }

        private static int CountDepthIncrements(int[] depths)
        {
            int count = 0;
            int previous = depths[0];
            for (int i = 1; i < depths.Length; i++)
            {
                if (depths[i] > previous)
                {
                    count++;
                }
                previous = depths[i];
            }
            return count;
        }

        private static int[] SumDepthsByWindow(int[] depths, int windowSize)
        {
            List<int> sums = new List<int>();
            for (int step = 0; step + windowSize - 1 < depths.Length; step++)
            {
                int currentSum = 0;
                for (int i = 0; i < windowSize; i++)
                {
                    currentSum += depths[step + i];
                }
                sums.Add(currentSum);
            }

            return sums.ToArray();
        }

        public static int CountDepthIncrements(string[] depths)
        {
            int[] convertedDepths = Array.ConvertAll(depths, d => int.Parse(d));
            return CountDepthIncrements((int[])convertedDepths);
        }

        public static int CountDepthIncrements(string[] depths, int windowSize)
        {
            int[] convertedDepths = Array.ConvertAll(depths, d => int.Parse(d));
            int[] sums = SumDepthsByWindow(convertedDepths, windowSize);
            return CountDepthIncrements(sums);
        }

        protected override void Run(string[] lines)
        {
            int measurementIncrements = CountDepthIncrements(lines);
            int windowedMeasurementIncrements = CountDepthIncrements(lines, 3);
            RegisterResultEntry($"measurement increments is {measurementIncrements}");
            RegisterResultEntry($"windowed increments is {windowedMeasurementIncrements}");
        }
    }
}