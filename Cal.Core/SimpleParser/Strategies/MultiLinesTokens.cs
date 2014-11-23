using System.Collections.Generic;
using Cal.Core.BlockParser;
using Cal.Core.Lexer;

namespace Cal.Core.SimpleParser.Strategies
{
    class MultiLinesTokens
    {
        private readonly List<TokenDef> _tokens;
        private readonly List<LineTokens> _lines;

        public MultiLinesTokens(List<TokenDef> tokens)
        {
            _tokens = tokens;
            _lines =new List<LineTokens>();
            SplitLines();
        }

        public List<LineTokens> Lines
        {
            get { return _lines; }
        }

        LineTokens NewLine(int start, int endRange)
        {
            var result = new LineTokens();
            for (var i = start; i <= endRange; i++)
            {
                result.Add(_tokens[i]);
            }
            result.CleanSpaces();
            if(result.Count>0)
                Lines.Add(result);
            return result;
        }

        private void SplitLines()
        {
            var pos = 0;
            while (true)
            {
                var startPos = pos;
                while (!IsEoln(pos))
                {
                    pos++;
                }
                if (startPos == pos)
                {
                    pos++; 
                    if (pos >= _tokens.Count)
                        return;
                    continue;
                }

                var newLine = NewLine(startPos, pos);
                if(pos>=_tokens.Count)
                    return;
            } 
        }

        private bool IsEoln(int pos)
        {
            if (pos >= _tokens.Count)
                return true;
            return _tokens[pos].Kind == TokenKind.Eoln ||
                _tokens[pos].Kind == TokenKind.Eof ||
                   _tokens[pos].Kind == TokenKind.OpSemiColon;
        }
    }
}