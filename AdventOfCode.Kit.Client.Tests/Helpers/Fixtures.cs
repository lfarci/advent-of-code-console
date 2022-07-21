﻿using AdventOfCode.Kit.Client.Resources;
using System.Collections.Generic;
using static AdventOfCode.Kit.Client.Resources.CalendarPage;

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
    }
}