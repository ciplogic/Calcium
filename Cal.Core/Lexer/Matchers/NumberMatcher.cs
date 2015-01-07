using Cal.Core.NewLexer;

namespace Cal.Core.Lexer.Matchers
{
    public class NumberMatcher:LexerMatcher
    {
        public override MatchResult GetMatchResult(string allText)
        {
            if (!DoesMatch(allText))
                return MatchResult.None;
            bool isFloat;
            var len = CalculateLengthNumber(allText, out isFloat);
            var result = new MatchResult(isFloat?TokenKind.Double:TokenKind.Integer, len);
            return result;
        }

        private static int CalculateLengthNumber(string allText, out bool isFloat)
        {
            var len = 0;
            isFloat = false;
            for (var i = 0; i < allText.Length; i++)
            {
                var ch = allText[i];
                if (!CharUtils.IsDigit(ch)
                    && ch != '.')
                {
                    len = i;
                    break;
                }
                if (ch == '.')
                    isFloat = true;
            }
            if (len == 0)
                len = allText.Length;
            return len;
        }

        public static bool DoesMatch(string allText)
        {
            return CharUtils.IsDigit(allText[0]);
        }
    }
}