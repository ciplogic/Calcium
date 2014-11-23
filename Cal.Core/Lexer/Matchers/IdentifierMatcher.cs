using Cal.Core.NewLexer;

namespace Cal.Core.Lexer.Matchers
{
    internal class IdentifierMatcher 
    {
        public static MatchResult ComputeMatchResult(string allText)
        {
            var len = 0;
            for (var i = 0; i < allText.Length; i++)
            {
                var ch = allText[i];
                if (!CharUtils.IsAlpha(ch)
                    && !CharUtils.IsDigit(ch)
                    && ch != '!'
                    && ch != '?')
                {
                    len = i;
                    break;
                }
            }
            if (len == 0)
                len = allText.Length;
            var result = new MatchResult(TokenKind.Identifier, len);
            return result;
        }

        public static bool ComputeDoesMatch(string allText)
        {
            return CharUtils.IsAlpha(allText[0]);
        }
    }
}