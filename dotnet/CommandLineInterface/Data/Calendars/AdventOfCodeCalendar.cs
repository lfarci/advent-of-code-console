using HtmlAgilityPack;

namespace CommandLineInterface.Data
{
    public class AdventOfCodeCalendar
    {
        private readonly IList<Day> _days;

        public IList<Day> Days { get => _days; }

        public AdventOfCodeCalendar(IList<Day> days)
        {
            _days = new List<Day>(days);
        }

        public AdventOfCodeCalendar() : this(new List<Day>())
        {
        }

        public void Add(int dayIndex, Completion completion)
        {
            _days.Add(new Day { Index = dayIndex, Completion = completion });
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

        public static AdventOfCodeCalendar Parse(string text)
        {
            var parsed = new AdventOfCodeCalendar();
            var document = new HtmlDocument();
            document.LoadHtml(text);

            var calendar = document.DocumentNode.SelectSingleNode("//main/pre[@class='calendar']");

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
            return obj is AdventOfCodeCalendar calendar &&
                   _days.All(day => calendar._days.Contains(day)) &&
                   _days.Distinct().Count() == _days.Count &&
                   _days.Count == calendar._days.Count;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_days);
        }

        public enum Completion
        {
            NotStarted = 0,
            Complete = 1,
            VeryComplete = 2
        }

        public struct Day
        {
            public int Index { get; set; }
            public Completion Completion { get; set; }

            public override bool Equals(object? obj)
            {
                return obj is Day day &&
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
