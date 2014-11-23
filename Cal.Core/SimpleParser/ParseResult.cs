using Cal.Core.BlockParser;

namespace Cal.Core.SimpleParser
{
    public class ParseResult
    {
        public bool HasErrors { get; set; }
        public AstNode Ast { get; set; }
    }
}