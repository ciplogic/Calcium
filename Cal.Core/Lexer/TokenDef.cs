namespace Cal.Core.Lexer
{
    public struct TokenDef
    {
        private readonly string _content;
        private readonly TokenKind _kind;
        private readonly int _row;
        private readonly int _column;
        private readonly int _length;

        public TokenDef(string content, TokenKind kind, int row, int column, int length)
        {
            _content = content.Substring(column, length);
            _kind = kind;
            _row = row;
            _column = column;
            _length = length;
        }

        public TokenDef(TokenKind kind, int row, int column)
        {
            _kind = kind;
            _row = row;
            _column = column;
            _length = 0;
            _content = string.Empty;
        }

        public TokenKind Kind { get { return _kind; } }

        public string GetContent()
        {
            if (string.IsNullOrEmpty(_content))
                return _content;
            var tokenContent = _content;
            return tokenContent;
        }
        public override string ToString()
        {
            return string.Format("'{0}' <{1}>", GetContent(), _kind);
        }
    }
}