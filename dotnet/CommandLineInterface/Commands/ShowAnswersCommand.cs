using AdventOfCode2021.CommandLineInterface.WebClient;
using AdventOfCode2021.CommandLineInterface.Data;
using CommandLineInterface.Data;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace CommandLineInterface.Commands
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
                ICalendarPageRepository calendars = CalendarPageRepository.Instance;
                IChallengeRepository challenges = ChallengeRepository.Instance;
                CalendarPage calendar = await calendars.FindByYear(year);

                var answers = new Table()
                    .Border(TableBorder.Rounded)
                    .BorderColor(Color.Green)
                    .AddColumn(new TableColumn("[u]Name[/]"))
                    .AddColumn(new TableColumn("[u]Part One[/]"))
                    .AddColumn(new TableColumn("[u]Part Two[/]"))
                    .AddColumn(new TableColumn("[u]Link[/]"));

                foreach (var day in calendar.Days)
                {

                    var challenge = challenges.FindByDay(day.Index);
                    var link = $"https://adventofcode.com/{year}/day/{day.Index}";


                    if (challenge != null)
                    {
                        answers.AddRow($"Day {day.Index}: {challenge.Title}", "0", "0", link);
                    }
                    else
                    { 
                        answers.AddRow($"Day {day.Index}", "", "", link);
                    }
                }

                AnsiConsole.Write(answers);

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
