using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class ValueExpression : ExprResolverBase
    {
        public ValueExpression(TokenDef contentToken) 
            : base(ExpressionKind.Constant)
        {
            Definition = contentToken;
        }

        public TokenDef Definition { get; set; }
        public override string ToCode()
        {
            return Definition.GetContent();
        }
    }
}