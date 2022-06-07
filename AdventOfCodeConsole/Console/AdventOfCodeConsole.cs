using Spectre.Console;

namespace CommandLineInterface.Console
{
    public static class AdventOfCodeConsole
    {
        public static void ShowErrorMessage(string message)
        {
            AnsiConsole.MarkupLine($"[red][bold]{message}[/][/]");
        }

        public static void Status(string message, Action action)
        {
            AnsiConsole.Status().Start(message, ctx => action());
        }

    }
}
