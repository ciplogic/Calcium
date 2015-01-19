using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.Definitions;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Definitions.ExpressionResolvers.Nodes;
using Cal.Core.Lexer;

namespace Cal.Core.RuntimeResolvers
{
    class CalRuntimeResolver : RuntimeResolverBase
    {
        public override bool CanResolveFunction(List<TokenDef> tokens, BlockDefinition instructionDefinition)
        {
            var tok0 = tokens[0].GetContent();
            switch (tok0)
            {
                case "puts":
                case "print":
                    return true;
            }
            return false;
        }

        public override ExprResolverBase FunctionResolve(List<TokenDef> contentTokens, BlockDefinition instructionDefinition)
        {
            return new RubyRuntimeFunctionResolved(contentTokens);
        }
    }
}
