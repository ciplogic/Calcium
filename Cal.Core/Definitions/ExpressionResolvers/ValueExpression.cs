using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class ValueExpression : ExprResolverBase
    {
        public ValueExpression(TokenDef contentToken)
        {
            Definition = contentToken;
        }

        public TokenDef Definition { get; set; }
    }
}