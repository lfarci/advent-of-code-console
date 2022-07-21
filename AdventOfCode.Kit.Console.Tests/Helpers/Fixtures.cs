using AdventOfCode.Kit.Client.Core;
using AdventOfCode.Kit.Console.Core;
using System.Collections.Generic;

namespace AdventOfCodeConsole.Tests.Helpers
{
    internal class Fixtures
    {
        public static readonly int DefaultYear = 2021;
        public static readonly int DefaultDay = 1;

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