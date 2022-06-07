using AdventOfCode.Console.Core;
using System.Collections.Generic;
using static AdventOfCode.Console.Web.Resources.CalendarPage;

namespace AdventOfCodeConsole.Tests.Helpers
{
    internal class Fixtures
    {
        public static readonly int DefaultYear = 2021;
        public static readonly int DefaultDay = 1;

        public static readonly IList<DayEntry> CalendarDays = new List<DayEntry>() {
            new DayEntry { Index = 1, Completion = Completion.VeryComplete },
            new DayEntry { Index = 2, Completion = Completion.VeryComplete },
            new DayEntry { Index = 3, Completion = Completion.VeryComplete },
            new DayEntry { Index = 4, Completion = Completion.VeryComplete },
            new DayEntry { Index = 5, Completion = Completion.VeryComplete },
            new DayEntry { Index = 6, Completion = Completion.VeryComplete },
            new DayEntry { Index = 7, Completion = Completion.VeryComplete },
            new DayEntry { Index = 8, Completion = Completion.VeryComplete },
            new DayEntry { Index = 9, Completion = Completion.VeryComplete },
            new DayEntry { Index = 10, Completion = Completion.Complete },
            new DayEntry { Index = 11, Completion = Completion.NotStarted },
            new DayEntry { Index = 12, Completion = Completion.NotStarted },
            new DayEntry { Index = 13, Completion = Completion.NotStarted },
            new DayEntry { Index = 14, Completion = Completion.NotStarted },
            new DayEntry { Index = 15, Completion = Completion.NotStarted },
            new DayEntry { Index = 16, Completion = Completion.NotStarted },
            new DayEntry { Index = 17, Completion = Completion.NotStarted },
            new DayEntry { Index = 18, Completion = Completion.NotStarted },
            new DayEntry { Index = 19, Completion = Completion.NotStarted },
            new DayEntry { Index = 20, Completion = Completion.NotStarted },
            new DayEntry { Index = 21, Completion = Completion.NotStarted },
            new DayEntry { Index = 22, Completion = Completion.NotStarted },
            new DayEntry { Index = 23, Completion = Completion.NotStarted },
            new DayEntry { Index = 24, Completion = Completion.NotStarted },
            new DayEntry { Index = 25, Completion = Completion.NotStarted }
        };

        public static readonly Calendar Calendar = new Calendar
        {
            Year = 2021,
            Length = 10,
            Days = new[] {
                new Day { Index = 1, Title = "Day 1" },
                new Day { Index = 2, Title = "Day 2" },
                new Day { Index = 3, Title = "Day 3"},
                new Day { Index = 4, Title = "Day 4"},
                new Day { Index = 5, Title = "Day 5"},
                new Day { Index = 6, Title = "Day 6"},
                new Day { Index = 7, Title = "Day 7"},
                new Day { Index = 8, Title = "Day 8"},
                new Day { Index = 9, Title = "Day 9"},
                new Day { Index = 10, Title = "Day 10" },
            }
        };

        public class FirstPuzzle : Puzzle
        {
            public override (Answer First, Answer Second) Run(string[] lines)
            {
                return (
                    new Answer { Value = 0, Description = "First" },
                    new Answer { Value = 1, Description = "Second" }
                );
            }
        }


        public class SecondPuzzle : Puzzle
        {
            public override (Answer First, Answer Second) Run(string[] lines)
            {
                return (
                    new Answer { Value = 0, Description = "First" },
                    new Answer { Value = 1, Description = "Second" }
                );
            }
        }

        public class ThirdPuzzle : Puzzle
        {
            public override (Answer First, Answer Second) Run(string[] lines)
            {
                return (
                    new Answer { Value = 0, Description = "First" },
                    new Answer { Value = 1, Description = "Second" }
                );
            }

        }

        public class PuzzleWithPrivateConstructor : Puzzle
        {
            private PuzzleWithPrivateConstructor()
            {
            }

            public override (Answer First, Answer Second) Run(string[] lines)
            {
                return (
                    new Answer { Value = 0, Description = "First" },
                    new Answer { Value = 1, Description = "Second" }
                );
            }

        }

    }
}