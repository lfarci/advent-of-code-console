using AdventOfCode.Console.Core;

namespace AdventOfCode.Console.View
{
    internal interface IAdventOfCodeView
    {
        void ShowMessage(string message);
        void ShowError(string message);
        void ShowPuzzleAnswers(Day day, string[] lines);
        void Status(string message, Action action);
    }
}
