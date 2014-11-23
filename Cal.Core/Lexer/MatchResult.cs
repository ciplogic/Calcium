using Cal.Core.NewLexer;

namespace Cal.Core.Lexer
{
    public struct MatchResult
    {
        public MatchResult(TokenKind kind, int length) : this()
        {
            Kind = kind;
            Length = length;
        }

        public int Length;
        public TokenKind Kind;
        static MatchResult ()
        {
            None = new MatchResult(TokenKind.None, 0);
        }

        public override string ToString()
        {
            return string.Format("Len = {0} Kind={1}", Length, Kind);
        }

        public static MatchResult None;
    }
}