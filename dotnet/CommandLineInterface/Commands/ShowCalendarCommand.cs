using AdventOfCode2021.CommandLineInterface.Client;
using CommandLineInterface.Data;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineInterface.Commands
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
                ICalendarRepository repository = CalendarRepository.Instance;
                AdventOfCodeCalendar calendar = await repository.FindByYear(year);

                var rule = new Rule($"Advent Of Code {year}");
                AnsiConsole.Write(rule);

                double veryComplete = 0.0;
                double complete = 0.0;
                double notStarted = 0.0;

                foreach (var day in calendar.Days)
                {
                    AnsiConsole.MarkupLine($"Day {day.Index}: {day.Completion}");

                    if (day.Completion == AdventOfCodeCalendar.Completion.Complete)
                    {
                        complete++;
                    }

                    if (day.Completion == AdventOfCodeCalendar.Completion.VeryComplete)
                    {
                        veryComplete++;
                    }

                    if (day.Completion == AdventOfCodeCalendar.Completion.NotStarted)
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
            catch (AdventOfCodeClientException)
            {
                AnsiConsole.MarkupLine($"[red][bold]Failed to show calendar for the given year:[/] {settings.Year}[/]");
                return -1;
            }
        }
    }
}
