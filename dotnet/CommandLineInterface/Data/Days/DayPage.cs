using HtmlAgilityPack;

namespace CommandLineInterface.Data
{
    public class DayPage
    {
        public static readonly string TitleNotFoundError = "Title not found.";
        public static readonly string InvalidTitleFormatError = "Invalid title";
        public static readonly string UnexpectedPuzzleAnswersCountError = "Unexpected count of puzzles answers";
        public static readonly string InvalidPuzzleAnswerFormatError = "Invalid puzzle answer format";

        private static readonly string titleSeparator = ":";
        private static readonly string titleDecorator = "-";
        private static readonly string puzzleAnswerText = "Your puzzle answer was";

        private static readonly string titleXpathQuery = "//main/article[@class='day-desc']/h2";
        private static readonly string puzzleAnswersQuery = $"//main/p[contains(text(), '{puzzleAnswerText}')]/code";
        private static readonly string descriptionsQuery = "//main/article[@class='day-desc']";

        public string Title { get; set; } = "";
        public Completion Completion { get; set; } = Completion.NotStarted;
        public int? FirstPuzzleAnswer { get; set; } = null;
        public int? SecondPuzzleAnswer { get; set; } = null;

        public static string ParseTitle(string text)
        {
            string[] tokens = text.Split(titleSeparator);
            if (tokens.Length != 2)
            {
                throw new FormatException($"{InvalidTitleFormatError}: {text}");
            }
            return tokens[1].Replace(titleDecorator, "").Trim();
        }

        public static bool HasSubmissionForm(HtmlNode document)
        { 
            return document.SelectSingleNode("//main/form[@method='post']") != null;
        }

        public static DayPage Parse(string text)
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

        private static void ParsePuzzleAnswers(DayPage dayPage, HtmlNode document)
        {
            var puzzleAnswerNodes = document.SelectNodes(puzzleAnswersQuery);
            int expectedAnswersCount = (int)dayPage.Completion;

            if (expectedAnswersCount != puzzleAnswerNodes?.Count)
            {
                throw new FormatException(UnexpectedPuzzleAnswersCountError);
            }

            try
            {
                if (puzzleAnswerNodes?.Count > 0)
                {
                    dayPage.FirstPuzzleAnswer = int.Parse(puzzleAnswerNodes[0].InnerText);
                }

                if (puzzleAnswerNodes?.Count == 2)
                {
                    dayPage.SecondPuzzleAnswer = int.Parse(puzzleAnswerNodes[1].InnerText);
                }
            }
            catch (FormatException e)
            {
                throw new FormatException($"{InvalidPuzzleAnswerFormatError}", e);
            }
        }


    }
}
