using System.Collections.Generic;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class AssignLeftDefinition : ExpressionDefinition
    {
        public AssignLeftDefinition(List<TokenDef> tokens, InstructionDefinition parent) : base(tokens, parent)
        {
        }
    }
}