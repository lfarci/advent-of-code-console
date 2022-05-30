using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            catch (FormatException)
            {
                return -1;
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
            var children = calendar.Descendants("a");

            foreach (var dayAnchorNode in children)
            {
                var day = ParseDayIndex(dayAnchorNode);
                var completion = ParseCompletion(dayAnchorNode);

                parsed.Add(day, completion);
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
