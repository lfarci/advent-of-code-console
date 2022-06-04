using AdventOfCode.Challenges;
using System;
using Xunit;

namespace Tests.Challenges.Day03
{
    public class BinaryDiagnosticChallengeTests
    {

        private readonly string[] diagnosticReport = {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        [Fact]
        public void DecodeGammaRateFrom_EmptyReport_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => BinaryDiagnosticChallenge.DecodeGammaRateFrom(Array.Empty<string>()));
        }

        [Fact]
        public void DecodeGammaRateFrom_ValidReport_ReturnsGammaRate()
        {
            string gammaRate = BinaryDiagnosticChallenge.DecodeGammaRateFrom(diagnosticReport);
            Assert.Equal("10110", gammaRate);
        }

        [Fact]
        public void DecodeGammaRateFrom_ValidReport_ReturnsSameLengthAsInputStrings()
        {
            string gammaRate = BinaryDiagnosticChallenge.DecodeGammaRateFrom(diagnosticReport);
            Assert.Equal(diagnosticReport[0].Length, gammaRate.Length);
        }

        [Fact]
        public void ConvertToEpsilonRate_ValidReport_ReturnsEpsilonRate()
        {
            string epsilonRate = BinaryDiagnosticChallenge.ConvertToEpsilonRate("10110");
            Assert.Equal("01001", epsilonRate);
        }

        [Fact]
        public void DecodePowerConsumption_ValidReport_ReturnsPowerConsumption()
        {
            int powerConsumption = BinaryDiagnosticChallenge.DecodePowerConsumption(diagnosticReport);
            Assert.Equal(198, powerConsumption);
        }

        [Fact]
        public void DecodeOxygenGeneratorRating_ValidReport_OxygenGeneratorRating()
        {
            int oxygenGeneratorRating = BinaryDiagnosticChallenge.DecodeOxygenGeneratorRating(diagnosticReport);
            Assert.Equal(23, oxygenGeneratorRating);
        }

        [Fact]
        public void DecodeCO2ScrubberRating_ValidReport_ReturnsCO2ScrubberRating()
        {
            int co2ScrubberRating = BinaryDiagnosticChallenge.DecodeCO2ScrubberRating(diagnosticReport);
            Assert.Equal(10, co2ScrubberRating);
        }

    }
}
