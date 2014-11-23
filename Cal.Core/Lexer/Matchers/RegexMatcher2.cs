using Cal.Core.Lexer;

namespace Cal.Core.NewLexer.Matchers
{
    public class RegexMatcher2 : LexerMatcher
    {
        public override MatchResult GetMatchResult(string allText)
        {
            if (!DoesMatch(allText))
                return MatchResult.None;

            var length = allText.IndexOf("/)")+2;
            var parenRegex = new MatchResult(TokenKind.ParenRegexValue, length);
            return parenRegex;
        }
        static readonly char[] CharsToStart = "(/".ToCharArray();
        public static bool DoesMatch(string allText)
        {
            var isCandidate = QuickStartsWith(allText, CharsToStart);
            if (!isCandidate)
                return false;
            return allText.Contains("/)");
        }
    }
}