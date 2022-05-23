using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Challenges
{
    public class SonarSweepChallenge : IDailyChallenge
    {

        private readonly string InputFileResourceName = "AdventOfCode2021.Resources.SonarSweepInput.txt";

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
            return CountDepthMeasurementIncrements((int[]) convertedDepths);
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

        public async Task Run()
        {
            string[] lines = await DailyChallengeInputResolver.ReadAllLinesFrom(2021, 1);
            int measurementIncrements = CountDepthMeasurementIncrements(lines);
            int windowedMeasurementIncrements = CountDepthMeasurementIncrements(lines, 3);
            Console.WriteLine($"Day 01 - SonarSweep:");
            Console.WriteLine($"- Part 1: Measurement increments: {measurementIncrements}");
            Console.WriteLine($"- Part 2: Windowed increments: {windowedMeasurementIncrements}");
        }
    }
}