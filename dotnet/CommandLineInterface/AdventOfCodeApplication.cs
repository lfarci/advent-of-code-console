using AdventOfCode.Console.Core;
using AdventOfCode.Console.Commands;
using Spectre.Console.Cli;
using CommandLineInterface.Console;

namespace AdventOfCode.Console
{
    public class AdventOfCodeApplication
    {
        private static AdventOfCodeApplication? instance;
        private IDictionary<int, AdventOfCodeContext> _contextsByYear;

        public static AdventOfCodeApplication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdventOfCodeApplication();
                }
                return instance;
            }
        }

        protected AdventOfCodeApplication()
        {
            _contextsByYear = new Dictionary<int, AdventOfCodeContext>();
        }

        public Calendar FindCalendar(int year)
        {
            if (!_contextsByYear.ContainsKey(year))
            {
                throw new ArgumentOutOfRangeException($"No calendar for year: {year}");
            }
            return _contextsByYear[year].Calendar;
        }

        public void StartYear(int year, Action<AdventOfCodeContext> onYearInitialized)
        {
            if (_contextsByYear.ContainsKey(year))
            {
                throw new InvalidOperationException($"Trying to add the same year twice: {year}.");
            }

            try
            {
                var context = new AdventOfCodeContext(year);
                _contextsByYear[year] = context;
                AdventOfCodeConsole.Status($"Initializing year {year}...", () =>
                {
                    context.Initialize(onYearInitialized).Wait();
                });
            }
            catch (Exception)
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
