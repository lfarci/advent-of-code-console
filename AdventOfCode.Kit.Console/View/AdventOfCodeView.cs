using AdventOfCode.Kit.Client.Core;
using Spectre.Console;

namespace AdventOfCode.Kit.Console.View
{
    public class AdventOfCodeView : IAdventOfCodeView
    {
        public void Status(string message, Action action)
        {
            AnsiConsole.Status().Start(message, ctx => action());
        }

        public void ShowError(string message)
        {
            AnsiConsole.MarkupLine($"[red][bold]{message}[/][/]");
        }

        public void ShowMessage(string message)
        {
            AnsiConsole.MarkupLine(message);
        }

        public void ShowPuzzleAnswers(Day day, string[] input)
        {
            AnsiConsole.MarkupLine($"Day {day.Index} - {day.Title}:");
            if (day.Puzzle == null)
            {
                AnsiConsole.MarkupLine($"No answers.");
            }
            else
            {
                var (First, Second) = day.Puzzle.Run(input);
                AnsiConsole.MarkupLine($"- {First.Description}: {First.Value}");
                AnsiConsole.MarkupLine($"- {Second.Description}: {Second.Value}");
            }
        }
    }
}
