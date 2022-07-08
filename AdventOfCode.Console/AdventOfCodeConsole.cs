using AdventOfCode.Console.Core;
using AdventOfCode.Console.Web;
using AdventOfCode.Console.Commands;
using Spectre.Console.Cli;
using AdventOfCode.Console.View;

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

        internal IAdventOfCodeView Console { get; init; }
        internal IDictionary<int, IPuzzleSubmitter> Submitters { get; init; }

        internal AdventOfCodeConsole() : this(new AdventOfCodeView())
        {}

        internal AdventOfCodeConsole(IAdventOfCodeView console)
        {
            Console = console;
            Submitters = new Dictionary<int, IPuzzleSubmitter>();
        }

        private bool HasSubmitterFor(int year) => Submitters.ContainsKey(year);

        internal IPuzzleSubmitter FindSubmitter(int year)
        {
            if (!HasSubmitterFor(year))
            {
                throw new InvalidOperationException($"No context for year: {year}");
            }
            return Submitters[year];
        }

        public void StartYear(int year, Action<IPuzzleSubmitter> onYearInitialized)
        {
            if (HasSubmitterFor(year))
            {
                throw new InvalidOperationException($"Trying to add the same year twice: {year}.");
            }

            try
            {
                IPuzzleSubmitter context = new AdventOfCodeContext(year);
                Submitters[year] = context;
                Console.Status($"Initializing year {year}...", () =>
                {
                    ((AdventOfCodeContext) context).Initialize(onYearInitialized).Wait();
                });
            }
            catch (Exception)
            {
                Console.ShowError($"Failed to add year {year}.");
            }
        }

        public void ShowPuzzleAnswers(int year, int dayIndex)
        {
            Console.Status($"Downloading input for year {year} and day {dayIndex}...", () =>
            {
                IResourceRepository repository = ResourceRepository.Instance;
                string[] lines = repository.FindPuzzleInputByYearAndDayAsync(year, dayIndex).Result;
                var submitter = FindSubmitter(year);

                if (submitter.Calendar != null)
                {
                    var day = submitter.Calendar[dayIndex];
                    Console.ShowPuzzleAnswers(day, lines);
                }
                else
                { 
                    Console.ShowError($"Could not run submitted puzzle because year {year} isn't initialized.");
                }
            });
        }

        public int Run(string[] args)
        {
            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddCommand<ShowPuzzleAnswers>("run")
                    .WithDescription("Runs the submitted puzzle and shows its answers.")
                    .WithExample(new[] { "run", "2020", "1" });
            });
            return app.Run(args);
        }
    }
}
