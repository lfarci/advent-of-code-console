using AdventOfCode.Kit.Console.Core;
using AdventOfCode.Kit.Console.Web.Resources;
using System.Collections.Generic;
using static AdventOfCode.Kit.Console.Web.Resources.CalendarPage;

namespace AdventOfCodeConsole.Tests.Helpers
{
    internal class Fixtures
    {
        public static readonly int DefaultYear = 2021;
        public static readonly int DefaultDay = 1;

        public static readonly DayPage DayPage = new DayPage
        {
            Title = "My sample say page",
            Completion = Completion.Complete,
            Index = 1,
            FirstPuzzleAnswer = 1324431,
            SecondPuzzleAnswer = 23432
        };

        public static readonly IList<DayEntry> CalendarPageEntries = new List<DayEntry>() {
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

        public static readonly CalendarPage CalendarPage = new CalendarPage(Fixtures.CalendarPageEntries);


        public static readonly IEnumerable<Day> CalendarDays = new[] {
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
                new Day { Index = 11, Title = "Day 11" },
                new Day { Index = 12, Title = "Day 12" },
                new Day { Index = 13, Title = "Day 13" },
                new Day { Index = 14, Title = "Day 14" },
                new Day { Index = 15, Title = "Day 15" },
                new Day { Index = 16, Title = "Day 16" },
                new Day { Index = 17, Title = "Day 17" },
                new Day { Index = 18, Title = "Day 18" },
                new Day { Index = 19, Title = "Day 19" },
                new Day { Index = 20, Title = "Day 20" },
                new Day { Index = 21, Title = "Day 21" },
                new Day { Index = 22, Title = "Day 22" },
                new Day { Index = 23, Title = "Day 23" },
                new Day { Index = 24, Title = "Day 24" },
                new Day { Index = 25, Title = "Day 25" },
            };

        public static readonly Calendar Calendar = new()
        {
            Year = 2021,
            Length = 25,
            Days = CalendarDays
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