using AdventOfCode.CommandLineInterface.Core;
using CommandLineInterface.Commands;
using Spectre.Console.Cli;

namespace AdventOfCode.CommandLineInterface
{
    public class AdventOfCodeApplication
    {

        private static readonly IPuzzleRepository puzzleRepository = PuzzleRepository.Instance;
        private IDictionary<int, AdventOfCodeContext> _contexts;

        public AdventOfCodeApplication()
        {
            _contexts = new Dictionary<int, AdventOfCodeContext>();
        }

        public void AddYear(int year, Action<AdventOfCodeContext> onYearInitialized)
        {
            if (_contexts.ContainsKey(year))
            {
                throw new InvalidOperationException($"Trying to add the same year twice: {year}.");
            }
            var context = new AdventOfCodeContext(year);
            _contexts[year] = context;

            Console.WriteLine("Loading...");
            context.Initialize(onYearInitialized).Wait();
            Console.WriteLine("Resources downloaded.");
        }

        public int Run(string[] args)
        {
            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddCommand<ShowCalendarCommand>("calendar")
                    .WithDescription("Show the Advent Of Code calendar for the given year.")
                    .WithExample(new[] { "calendar", "2020" });
                config.AddCommand<ShowAnswersCommand>("answers")
                    .WithDescription("Show the Advent Of Code answers for the given year.")
                    .WithExample(new[] { "answers", "2020" });
            });
            return app.Run(args);
        }
    }
}
