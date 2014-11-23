using Cal.Core.Lexer;

namespace Cal.Core.NewLexer.Matchers
{
    public class SpacesMatcher : LexerMatcher
    {
        static readonly char[] _toSkip = { ' ', '\t' };
        public override MatchResult GetMatchResult(string allText)
        {
            if (!DoesMatch(allText))
                return MatchResult.None;
            var trimLength = 0;
            for (var index = 0; index < allText.Length; index++)
            {
                var ch = allText[index];
                if(!IsSpacedChar(ch))
                    break;
                trimLength++;
            }

            return new MatchResult(TokenKind.Spaces, trimLength);
        }

        public bool DoesMatch(string allText)
        {
            var firstChar = allText[0];
            return IsSpacedChar(firstChar);
        }

        private static bool IsSpacedChar(char firstChar)
        {
            for (int index = 0; index < _toSkip.Length; index++)
            {
                var c = _toSkip[index];
                if (c == firstChar) return true;
            }
            return false;
        }
    }
}