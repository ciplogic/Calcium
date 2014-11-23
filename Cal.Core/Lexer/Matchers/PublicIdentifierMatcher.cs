using Cal.Core.Lexer;
using Cal.Core.Lexer.Matchers;

namespace Cal.Core.NewLexer.Matchers
{
    public class PublicIdentifierMatcher : LexerMatcher
    {
        public override MatchResult GetMatchResult(string allText)
        {
            if (!DoesMatch(allText))
                return MatchResult.None;
            var subText = allText.Substring(1);
            var identifierResult = IdentifierMatcher.ComputeMatchResult(subText);
            return new MatchResult(TokenKind.PublicIdentifier, identifierResult.Length+1);
        }

        private static bool DoesMatch(string allText)
        {
        	if (allText[0]!=':')
                return false;
            if (allText.Length<2)
                return false;
            var subText = allText.Substring(1);
            return IdentifierMatcher.ComputeDoesMatch(subText);
        }
    }
}