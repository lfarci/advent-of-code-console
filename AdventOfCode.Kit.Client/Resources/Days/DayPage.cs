using HtmlAgilityPack;

namespace AdventOfCode.Kit.Client.Resources
{
    public class DayPage
    {
        internal static readonly string TitleNotFoundError = "Title not found.";
        internal static readonly string InvalidTitleFormatError = "Invalid title";
        internal static readonly string UnexpectedPuzzleAnswersCountError = "Unexpected count of puzzles answers";
        internal static readonly string InvalidPuzzleAnswerFormatError = "Invalid puzzle answer format";

        private static readonly string titleSeparator = ":";
        private static readonly string titleDecorator = "-";
        private static readonly string puzzleAnswerText = "Your puzzle answer was";

        private static readonly string titleXpathQuery = "//main/article[@class='day-desc']/h2";
        private static readonly string puzzleAnswersQuery = $"//main/p[contains(text(), '{puzzleAnswerText}')]/code";
        private static readonly string descriptionsQuery = "//main/article[@class='day-desc']";

        public int Index { get; set; } = 1;
        public string Title { get; set; } = "";
        public Completion Completion { get; set; } = Completion.NotStarted;
        public long? FirstPuzzleAnswer { get; set; } = null;
        public long? SecondPuzzleAnswer { get; set; } = null;

        internal static string ParseTitle(string text)
        {
            string[] tokens = text.Split(titleSeparator);
            if (tokens.Length != 2)
            {
                throw new FormatException($"{InvalidTitleFormatError}: {text}");
            }
            return tokens[1].Replace(titleDecorator, "").Trim();
        }

        internal static bool HasSubmissionForm(HtmlNode document)
        {
            return document.SelectSingleNode("//main/form[@method='post']") != null;
        }

        internal static DayPage Parse(string text)
        {
            var document = new HtmlDocument();
            var dayPage = new DayPage();
            document.LoadHtml(text);

            dayPage.Title = ParseTitle(document.DocumentNode);
            dayPage.Completion = ParseCompletion(document.DocumentNode);

            if (dayPage.Completion != Completion.NotStarted)
            {
                ParsePuzzleAnswers(dayPage, document.DocumentNode);
            }

            return dayPage;
        }

        private static string ParseTitle(HtmlNode document)
        {
            var titleNode = document.SelectSingleNode(titleXpathQuery);

            if (titleNode == null)
            {
                throw new FormatException(TitleNotFoundError);
            }

            return ParseTitle(titleNode.InnerText);
        }

        private static Completion ParseCompletion(HtmlNode document)
        {
            var completion = Completion.NotStarted;
            var descriptions = document.SelectNodes(descriptionsQuery);

            if (descriptions?.Count == 2)
            {
                completion = HasSubmissionForm(document) ? Completion.Complete : Completion.VeryComplete;
            }
            return completion;
        }

        private static long ParsePuzzleAnswer(string text)
        {
            try
            {
                return long.Parse(text);
            }
            catch (Exception e)
            when (e is FormatException || e is OverflowException || e is ArgumentNullException)
            {
                throw new FormatException($"{InvalidPuzzleAnswerFormatError}: \"{text}\"", e);
            }
        }

        private static void ParsePuzzleAnswers(DayPage dayPage, HtmlNode document)
        {
            var puzzleAnswerNodes = document.SelectNodes(puzzleAnswersQuery);
            int expectedAnswersCount = (int)dayPage.Completion;

            if (expectedAnswersCount != puzzleAnswerNodes?.Count)
            {
                throw new FormatException(UnexpectedPuzzleAnswersCountError);
            }

            if (puzzleAnswerNodes?.Count > 0)
            {
                dayPage.FirstPuzzleAnswer = ParsePuzzleAnswer(puzzleAnswerNodes[0].InnerText);
            }

            if (puzzleAnswerNodes?.Count == 2)
            {
                dayPage.SecondPuzzleAnswer = ParsePuzzleAnswer(puzzleAnswerNodes[1].InnerText);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is DayPage page &&
                   Index == page.Index &&
                   Title == page.Title &&
                   Completion == page.Completion &&
                   FirstPuzzleAnswer == page.FirstPuzzleAnswer &&
                   SecondPuzzleAnswer == page.SecondPuzzleAnswer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Title, Completion, FirstPuzzleAnswer, SecondPuzzleAnswer);
        }
    }
}
