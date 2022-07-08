using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Console.Commands
{
    public class ShowPuzzleAnswers : Command<ShowPuzzleAnswers.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[year]")]
            [Description("The year to show the Advent Of Code calendar for.")]
            public string? Year { get; init; }

            [CommandArgument(1, "[day]")]
            [Description("The day to show the Advent Of Code calendar for.")]
            public string? Day { get; init; }
        }

        private const int V = 0;
        private AdventOfCodeConsole _console = AdventOfCodeConsole.Instance;

        public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
        {
            try
            {
                int year = int.Parse(settings?.Year ?? "");
                int day = int.Parse(settings?.Day ?? "");
                _console.ShowPuzzleAnswers(year, day);
                return 0;
            }
            catch (FormatException)
            {
                AnsiConsole.MarkupLine($"[red][bold]Invalid year:[/] {settings.Year}[/]");
                return -1;
            }
            catch (IOException)
            {
                AnsiConsole.MarkupLine($"[red][bold]Failed to show calendar for the given year:[/] {settings.Year}[/]");
                return -1;
            }

        }
    }
}
