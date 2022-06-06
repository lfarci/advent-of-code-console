
using AdventOfCode.Challenges;
using AdventOfCode.Console;

namespace AdventOfCode
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var app = new AdventOfCodeApplication();

            app.AddYear(2020, year => {
                year.Submit<SonarSweepChallenge>().Run(1);
                year.Submit<DiveChallenge>().Run(2);
                year.Submit<BinaryDiagnosticChallenge>().Run(3);
            });

            app.AddYear(2021, year => {
                year.Submit<SonarSweepChallenge>().Run(1);
                year.Submit<DiveChallenge>().Run(2);
                year.Submit<BinaryDiagnosticChallenge>().Run(3);
            });

            app.Run(args);
        }
    }
}