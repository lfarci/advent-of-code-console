using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Challenges
{
    public class SonarSweepChallenge : DailyChallenge
    {

        private const string title = "Sonar Sweep";
        private const int day = 1;

        public SonarSweepChallenge() : base(title, day)
        {
        }

        int CountDepthMeasurementIncrements(int[] depths)
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

        int CountDepthMeasurementIncrements(string[] depths)
        {
            int[] convertedDepths = Array.ConvertAll(depths, d => int.Parse(d));
            return CountDepthMeasurementIncrements((int[])convertedDepths);
        }

        int[] SumDepthWindows(int[] depths, int windowSize)
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

        int CountDepthMeasurementIncrements(string[] depths, int windowSize)
        {
            int[] convertedDepths = Array.ConvertAll(depths, d => int.Parse(d));
            int[] sums = SumDepthWindows(convertedDepths, windowSize);
            return CountDepthMeasurementIncrements(sums);
        }

        protected override void Run(string[] lines)
        {
            int measurementIncrements = CountDepthMeasurementIncrements(lines);
            int windowedMeasurementIncrements = CountDepthMeasurementIncrements(lines, 3);
            RegisterResultEntry($"measurement increments is {measurementIncrements}");
            RegisterResultEntry($"windowed increments is {windowedMeasurementIncrements}");
        }
    }
}