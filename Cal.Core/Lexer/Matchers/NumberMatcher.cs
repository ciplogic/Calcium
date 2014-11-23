using Cal.Core.NewLexer;

namespace Cal.Core.Lexer.Matchers
{
    public class NumberMatcher:LexerMatcher
    {
        public override MatchResult GetMatchResult(string allText)
        {
            if (!DoesMatch(allText))
                return MatchResult.None;
            var len = 0;
            for (var i = 0; i < allText.Length; i++)
            {
                var ch = allText[i];
                if (!CharUtils.IsDigit(ch)
                    && ch != '.')
                {
                    len = i;
                    break;
                }
            }
            if (len == 0)
                len = allText.Length;
            var extractedText = ExtractMatchText(allText, len);
            var isFloat = extractedText.Contains(".");
            var result = new MatchResult(isFloat?TokenKind.Double:TokenKind.Integer, len);
            return result;
        }

        public static bool DoesMatch(string allText)
        {
            return CharUtils.IsDigit(allText[0]);
        }
    }
}