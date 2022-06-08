﻿using AdventOfCode.Console.Core;
using AdventOfCode.Console.Web.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace AdventOfCode.Console.Commands
{
    public class ShowAnswersCommand : AsyncCommand<ShowAnswersCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[year]")]
            [Description("The year to show the Advent Of Code calendar for.")]
            public string? Year { get; init; }
        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            try
            {


                int year = int.Parse(settings?.Year ?? "");
                AdventOfCodeConsole app = AdventOfCodeConsole.Instance;

                var answers = new Table()
                    .Border(TableBorder.Rounded)
                    .BorderColor(Color.Green)
                    .AddColumn(new TableColumn("[u]Name[/]"))
                    .AddColumn(new TableColumn("[u]Part One[/]"))
                    .AddColumn(new TableColumn("[u]Part Two[/]"))
                    .AddColumn(new TableColumn("[u]Link[/]"));

                foreach (var day in app.FindCalendar(year).Days)
                {
                    var link = $"https://adventofcode.com/{year}/day/{day.Index}";
                    answers.AddRow(day.Title, day.FirstPuzzleAnswer.ToString(), day.SecondPuzzleAnswer.ToString(), link);
                }

                AnsiConsole.Write(answers);

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
