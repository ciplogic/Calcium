using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class RubyRuntimeFunctionResolved : ExprResolverBase
    {
        private readonly List<TokenDef> _contentTokens;

        public RubyRuntimeFunctionResolved(List<TokenDef> contentTokens) : base(ExpressionKind.FunctionCall)
        {
            _contentTokens = contentTokens;
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }

        public override bool CalculateExpressionType()
        {
            throw new System.NotImplementedException();
        }
    }
}