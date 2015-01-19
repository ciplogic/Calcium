using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Definitions.ExpressionResolvers.Nodes;
using Cal.Core.Lexer;

namespace Cal.Core.RuntimeResolvers
{
    internal class CurrentClassResolver : RuntimeResolverBase
    {
        public override bool CanResolveFunction(List<TokenDef> tokens, BlockDefinition instructionDefinition)
        {
            if (tokens[0].Kind != TokenKind.Identifier)
                return false;
            if (tokens.Count < 3)
                return HandleLowCountTokens(tokens, instructionDefinition);

            if (tokens.Count < 3)
                return false;
            if (tokens[1].Kind != TokenKind.OpOpenParen)
                return false;

            return true;
        }

        private static bool HandleLowCountTokens(List<TokenDef> tokens, BlockDefinition instructionDefinition)
        {
            var classParent = instructionDefinition.GetClassParent();
            var functionName = tokens[0].GetContent();
            return classParent.Defs.Any(def => def.Name == functionName);
        }

        public override ExprResolverBase FunctionResolve(List<TokenDef> contentTokens, BlockDefinition instructionDefinition)
        {
            return new FunctionCallResolved(contentTokens, instructionDefinition);
        }
    }
}