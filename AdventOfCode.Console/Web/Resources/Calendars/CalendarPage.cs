using AdventOfCode.Console.Core;
using HtmlAgilityPack;

namespace AdventOfCode.Console.Web.Resources
{
    internal class CalendarPage
    {
        private readonly IList<DayEntry> _days;

        public IList<DayEntry> Days { get => _days; }
        public int Year { get; set; } = DateTime.Now.Year;

        public CalendarPage(IList<DayEntry> days)
        {
            _days = new List<DayEntry>(days);
        }

        public CalendarPage() : this(new List<DayEntry>())
        {
        }

        public void Add(int dayIndex, Completion completion)
        {
            _days.Add(new DayEntry { Index = dayIndex, Completion = completion });
        }

        public static int ParseDayIndex(HtmlNode dayAnchorNode)
        {
            var daySpanNode = dayAnchorNode.SelectSingleNode("span[@class='calendar-day']");
            try
            {
                string day = daySpanNode?.InnerHtml ?? "";
                return int.Parse(day);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Invalid day index format.", e);
            }
        }

        public static Completion ParseCompletion(HtmlNode dayAnchorNode)
        {
            var className = dayAnchorNode.Attributes["class"]?.Value;
            if (className != null && className.Contains("calendar-complete"))
            {
                return Completion.Complete;
            }
            if (className != null && className.Contains("calendar-verycomplete"))
            {
                return Completion.VeryComplete;
            }
            return Completion.NotStarted;
        }

        public static CalendarPage Parse(string text)
        {
            var parsed = new CalendarPage();
            var document = new HtmlDocument();
            document.LoadHtml(text);

            var calendar = document.DocumentNode.SelectSingleNode("//main/pre[contains(@class, 'calendar')]");

            if (calendar == null)
            {
                throw new FormatException("Could not find the calendar in the given page.");
            }

            var children = calendar.Descendants("a");

            if (!children.Any())
            {
                throw new FormatException("Could not find any entry in the calendar.");
            }

            foreach (var dayAnchorNode in children)
            {
                parsed.Add(ParseDayIndex(dayAnchorNode), ParseCompletion(dayAnchorNode));
            }

            return parsed;
        }

        public override bool Equals(object? obj)
        {
            return obj is CalendarPage calendar &&
                   _days.All(day => calendar._days.Contains(day)) &&
                   _days.Distinct().Count() == _days.Count &&
                   _days.Count == calendar._days.Count;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_days);
        }

        public struct DayEntry
        {
            public int Index { get; set; }
            public Completion Completion { get; set; }

            public override bool Equals(object? obj)
            {
                return obj is DayEntry day &&
                       Index == day.Index &&
                       Completion == day.Completion;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Index, Completion);
            }
        }
    }
}
