using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Challenges
{
    public class SonarSweepChallenge : IDailyChallenge
    {

        private readonly string InputFileResourceName = "AdventOfCode2021.Resources.SonarSweepInput.txt";

        int CountDepthMeasurementIncrement(int[] depths)
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

        int CountDepthMeasurementIncrement(string[] depths)
        {
            int[] convertedDepths = Array.ConvertAll(depths, d => int.Parse(d));
            return CountDepthMeasurementIncrement((int[])convertedDepths);
        }

        public void Run()
        {
            string[] lines = ChallengeResourceManager.ReadAllLinesFrom(InputFileResourceName);
            int measurementIncrement = CountDepthMeasurementIncrement(lines);
            Console.WriteLine($"Day 1: SonarSweep\n\nMeasurement increment: {measurementIncrement}");
        }
    }
}