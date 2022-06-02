using HtmlAgilityPack;

namespace CommandLineInterface.Data.Days
{
    public class Day
    {
        private static readonly string titleSeparator = ":";
        private static readonly string titleDecorator = "-";

        public string Title { get; set; } = "";

        public static string ParseTitle(string text)
        {
            string[] tokens = text.Split(titleSeparator);
            if (tokens.Length != 2)
            {
                throw new FormatException($"Invalid title text: {text}");
            }
            return tokens[1].Replace(titleDecorator, "").Trim();
        }

        public static Day Parse(string text)
        {
            var document = new HtmlDocument();
            document.LoadHtml(text);

            var titleNode = document.DocumentNode.SelectSingleNode("//main/article[@class='day-desc']/h2");

            if (titleNode == null)
            {
                throw new FormatException("Could find the h2 element in the page.");
            }

            string title = ParseTitle(titleNode.InnerText);

            return new Day { Title = title };
        }


    }
}
