using System.Text;

namespace AdventOfCode2021.Challenges
{


    public class BinaryDiagnosticChallenge : DailyChallenge
    {
        public BinaryDiagnosticChallenge(int day, string title) : base(title, day)
        {
        }

        public override IEnumerable<PuzzleAnswer> Run(string[] lines)
        {
            int powerConsumption = DecodePowerConsumption(lines);
            int oxygenGeneratorRating = DecodeOxygenGeneratorRating(lines);
            int co2ScrubberRating = DecodeCO2ScrubberRating(lines);
            return new List<PuzzleAnswer> {
                new PuzzleAnswer(powerConsumption, "power consumption"),
                new PuzzleAnswer(oxygenGeneratorRating * co2ScrubberRating, "oxygen generator rating multiplied by the CO2 scrubber rating")
            };
        }

        public static string DecodeGammaRateFrom(string[] diagnosticReport)
        {
            if (diagnosticReport.Length == 0)
            {
                throw new ArgumentException("Cannot decode gamma rate from an empty report.");
            }
            int[] enabledBitsCounters = CountEnabledBitsByPosition(diagnosticReport);
            StringBuilder sb = new StringBuilder();
            foreach (int positionCounter in enabledBitsCounters)
            {
                if (positionCounter > diagnosticReport.Length / 2)
                {
                    sb.Append('1');
                }
                else
                {
                    sb.Append('0');
                }
            }
            return sb.ToString();
        }

        private static int[] CountEnabledBitsByPosition(string[] diagnosticReport)
        {
            int wordSize = diagnosticReport[0].Length;
            int[] enabledBitsCounters = new int[wordSize];
            foreach (string binaryNumber in diagnosticReport)
            {
                for (int i = 0; i < binaryNumber.Length; i++)
                {
                    if (binaryNumber[i] == '1')
                    {
                        enabledBitsCounters[i]++;
                    }
                }
            }
            return enabledBitsCounters;
        }

        public static int DecodePowerConsumption(string[] diagnosticReport)
        {
            string gammaRate = DecodeGammaRateFrom(diagnosticReport);
            string epsilonRate = ConvertToEpsilonRate(gammaRate);
            try
            {
                return Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
            }
            catch (OverflowException e)
            {
                throw new ArgumentException("Rates could not be encoded as 32 bits integers.", e);
            }
            catch (FormatException e)
            {
                throw new ArgumentException("Invalid format for one of the rates.", e);
            }
        }

        private delegate char BitCriteriaProvider(int enabledBitCounter, int binaryNumberCount);

        private static string? FindRatingByBitCriteria(string[] diagnosticReport, BitCriteriaProvider getBitCriteria)
        {
            string[] remainings = diagnosticReport;
            int[] positionCounts = CountEnabledBitsByPosition(diagnosticReport);
            int currentPosition = 0;
            while (currentPosition < diagnosticReport.Length && remainings.Length > 1)
            {
                int currentPositionCount = positionCounts[currentPosition];
                char bitCriteria = getBitCriteria(currentPositionCount, remainings.Length);
                remainings = remainings.Where(b => b[currentPosition] == bitCriteria).ToArray();
                positionCounts = CountEnabledBitsByPosition(remainings);
                currentPosition++;
            }
            return remainings.Length == 1 ? remainings[0] : null;
        }

        public static int DecodeOxygenGeneratorRating(string[] diagnosticReport)
        {
            BitCriteriaProvider provider = (count, total) => count >= total / 2.0m ? '1' : '0';
            string? rating = FindRatingByBitCriteria(diagnosticReport, provider);
            if (rating == null) return 0;
            return Convert.ToInt32(rating, 2);
        }

        public static int DecodeCO2ScrubberRating(string[] diagnosticReport)
        {
            BitCriteriaProvider provider = (count, total) => count < total / 2.0m ? '1' : '0';
            string? rating = FindRatingByBitCriteria(diagnosticReport, provider);
            if (rating == null) return 0;
            return Convert.ToInt32(rating, 2);
        }

        public static string ConvertToEpsilonRate(string gammaRate)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char bit in gammaRate)
            {
                sb.Append(bit == '1' ? '0' : '1');
            }
            return sb.ToString();
        }


    }
}
