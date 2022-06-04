
using AdventOfCode.Challenges;
using AdventOfCode.CommandLineInterface;

namespace AdventOfCode
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var app = new AdventOfCodeApplication();

            Console.WriteLine("Adding year 2021");

            app.AddYear(2021, context => {

                Console.WriteLine("[Program] About to submit puzzles");

                context.Submit<SonarSweepChallenge>().ForDay(1);
                context.Submit<DiveChallenge>().ForDay(2);
                context.Submit<BinaryDiagnosticChallenge>().ForDay(3);

                Console.WriteLine("[Program] All puzzles have been registered");
            });

            Console.WriteLine("Year 2021 added");

            //app.Run(args);
        }
    }
}