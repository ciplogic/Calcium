using Cal.Core.Lexer;
using Cal.Core.Lexer.Matchers;

namespace Cal.Core.NewLexer.Matchers
{
    public class EnvironmentIdentifierMatcher : LexerMatcher
    {
        public override MatchResult GetMatchResult(string allText)
        {
        	if (allText[0] !='$')
            {
                return MatchResult.None;
            }
            if (allText.Length < 2)
                return MatchResult.None;
            var subText = allText.Substring(1);
            var identifierResult = IdentifierMatcher.ComputeMatchResult(subText);
            return new MatchResult(TokenKind.PublicIdentifier, identifierResult.Length + 1);
        }

        public bool DoesMatch(string allText)
        {
                return false;
            var subText = allText.Substring(1);
            return IdentifierMatcher.ComputeDoesMatch(subText);
        }
    }
}