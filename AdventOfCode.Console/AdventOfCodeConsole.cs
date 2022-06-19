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

        internal IDictionary<int, IPuzzleSubmitter> Contexts { get; init; }

        internal AdventOfCodeConsole()
        {
            Contexts = new Dictionary<int, IPuzzleSubmitter>();
        }

        private bool HasContextFor(int year) => Contexts.ContainsKey(year);

        internal IPuzzleSubmitter FindContext(int year)
        {
            if (!HasContextFor(year))
            {
                throw new InvalidOperationException($"No context for year: {year}");
            }
            return Contexts[year];
        }

        public void StartYear(int year, Action<IPuzzleSubmitter> onYearInitialized)
        {
            if (HasContextFor(year))
            {
                throw new InvalidOperationException($"Trying to add the same year twice: {year}.");
            }

            try
            {
                IPuzzleSubmitter context = new AdventOfCodeContext(year);
                Contexts[year] = context;
                CommandLineInterface.Console.AdventOfCodeConsole.Status($"Initializing year {year}...", () =>
                {
                    ((AdventOfCodeContext) context).Initialize(onYearInitialized).Wait();
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
