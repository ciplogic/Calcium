using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    internal class ValueExpressionString : ExprResolverBase
    {
        private readonly TokenDef _contentToken;

        public ValueExpressionString(TokenDef contentToken) 
            : base(ExpressionKind.Constant)
        {
            _contentToken = contentToken;
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }
    }
}