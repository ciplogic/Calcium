using System.Collections.Generic;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class ExpressionDefinition
    {
        private readonly List<TokenDef> _contentTokens;

        public ExpressionDefinition(List<TokenDef> contentTokens)
        {
            _contentTokens = contentTokens;
        }

        public List<TokenDef> ContentTokens
        {
            get { return _contentTokens; }
        }

        public ClassDefinition TypeDefinition { get; set; }
        public InstructionDefinition ParentDefinition { get; set; }

        public override string ToString()
        {
            return ContentTokens.TokenJoinContent(); 
        }
    }
}