﻿using AdventOfCode.Console.Core;
using AdventOfCode.Console.Web.Resources;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace AdventOfCode.Console.Commands
{
    public class ShowCalendarCommand : AsyncCommand<ShowCalendarCommand.Settings>
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
                ICalendarPageRepository repository = CalendarPageRepository.Instance;
                CalendarPage calendar = await repository.FindByYearAsync(year);

                var rule = new Rule($"Advent Of Code {year}");
                AnsiConsole.Write(rule);

                double veryComplete = 0.0;
                double complete = 0.0;
                double notStarted = 0.0;

                foreach (var day in calendar.Days)
                {
                    AnsiConsole.MarkupLine($"Day {day.Index}: {day.Completion}");

                    if (day.Completion == Completion.Complete)
                    {
                        complete++;
                    }

                    if (day.Completion == Completion.VeryComplete)
                    {
                        veryComplete++;
                    }

                    if (day.Completion == Completion.NotStarted)
                    {
                        notStarted++;
                    }

                }

                var count = calendar.Days.Count;

                var chart = new BreakdownChart()
                    .FullSize()
                    .Width(50)
                    .ShowPercentage()
                    .AddItem("Very complete", Math.Round(veryComplete / count * 100, 2), Color.LightGreen)
                    .AddItem("Complete", Math.Round(complete / count * 100, 2), Color.Green)
                    .AddItem("To solve", Math.Round(notStarted / count * 100, 2), Color.Grey);

                AnsiConsole.Write(new Panel(chart).Padding(1, 1).Header("Completion"));

                AnsiConsole.Write(new Rule());

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
