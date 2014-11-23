using System.IO;
using Cal.Core.Lexer;

namespace Cal.Core.NewLexer.Matchers
{
    public class DoubleQuoteMatcher : LexerMatcher
    {
        public override MatchResult GetMatchResult(string allText)
        {
            if (allText[0] != '\"')
                return MatchResult.None;

            if (allText.Length < 2)
                return MatchResult.None;

            var quickIndexOf = QuickIndexOf(allText, '\"', 1);
            if(quickIndexOf==-1)
                throw new InvalidDataException("String is not closed");
            return new MatchResult(
                TokenKind.OpSingleQuote,
                quickIndexOf + 1
                );
        }
    }
}