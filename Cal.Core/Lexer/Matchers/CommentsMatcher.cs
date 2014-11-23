using Cal.Core.NewLexer;

namespace Cal.Core.Lexer.Matchers
{
    public class CommentsMatcher : LambdaMatcher
    {
        public CommentsMatcher()
        	: base(allText => allText[0] == '#',
            allText => new MatchResult(TokenKind.Comment, allText.Length)
            )
        {
        }

    }
}