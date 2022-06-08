using AdventOfCode.Console.Core;
using AdventOfCode.Console.Commands;
using Spectre.Console.Cli;

namespace AdventOfCode.Console
{
    public class AdventOfCodeConsole
    {
        private static AdventOfCodeConsole? instance;
        public static AdventOfCodeConsole Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdventOfCodeConsole();
                }
                return instance;
            }
        }

        internal IDictionary<int, AdventOfCodeContext> Contexts { get; init; }

        internal AdventOfCodeConsole()
        {
            Contexts = new Dictionary<int, AdventOfCodeContext>();
        }

        private bool HasContextFor(int year) => Contexts.ContainsKey(year);

        internal AdventOfCodeContext FindContext(int year)
        {
            if (!HasContextFor(year))
            {
                throw new InvalidOperationException($"No context for year: {year}");
            }
            return Contexts[year];
        }

        public void StartYear(int year, Action<AdventOfCodeContext> onYearInitialized)
        {
            if (HasContextFor(year))
            {
                throw new InvalidOperationException($"Trying to add the same year twice: {year}.");
            }

            try
            {
                var context = new AdventOfCodeContext(year);
                Contexts[year] = context;
                CommandLineInterface.Console.AdventOfCodeConsole.Status($"Initializing year {year}...", () =>
                {
                    context.Initialize(onYearInitialized).Wait();
                });
            }
            catch (Exception)
            {
                CommandLineInterface.Console.AdventOfCodeConsole.ShowErrorMessage($"Failed to add year {year}.");
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
