using System.IO;
using Cal.Core.Lexer;
using Cal.Core.Lexer.Matchers;

namespace Cal.Core.NewLexer.Matchers
{
    public class RegexMatcher : LambdaMatcher
    {
        public RegexMatcher()
            : base(allText => QuickStartsWith(allText, '~'),
                allText => new MatchResult(TokenKind.RegexValue, allText.Length)
                )
        {
        }

    }
}