using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class CallDefinition : InstructionDefinition
    {
        public CallDefinition(List<TokenDef> tokenDefs, ScopeDefinition scope) : base(scope)
        {
            TokenDefs = tokenDefs;
        }

        public List<TokenDef> TokenDefs { get; set; }

        public override string ToString()
        {
            return TokenDefs.TokenJoinContent();
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.AppendLine(TokenDefs.TokenJoinContent() + ";"); 
        }
    }
}