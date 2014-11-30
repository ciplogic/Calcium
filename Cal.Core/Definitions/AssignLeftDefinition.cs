using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions
{
    public class AssignLeftDefinition 
    {
        private readonly List<TokenDef> _tokens;

        public AssignLeftDefinition(List<TokenDef> tokens)
        {
            _tokens = tokens;
        }

        public List<TokenDef> Tokens
        {
            get { return _tokens; }
        }
    }
}