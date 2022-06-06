using AdventOfCode.Console.Core;
using AdventOfCode.Console.Commands;
using Spectre.Console.Cli;
using CommandLineInterface.Console;

namespace AdventOfCode.Console
{
    public class AdventOfCodeApplication
    {

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

            try
            {
                var context = new AdventOfCodeContext(year);
                _contexts[year] = context;
                AdventOfCodeConsole.Status($"Initializing year {year}...", () =>
                {
                    context.Initialize(onYearInitialized).Wait();
                });
            }
            catch (Exception e) when (e is InvalidOperationException || e is IOException)
            {
                AdventOfCodeConsole.ShowErrorMessage($"Failed to add year {year}.");
            }
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
