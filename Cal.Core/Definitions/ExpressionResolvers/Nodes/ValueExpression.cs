using System;
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

        public override bool CalculateExpressionType()
        {
            switch (Definition.Kind)
            {
                case TokenKind.Double:
                    ExpressionType = new ClrClassDefinition(typeof(double));
                    return true;
                case TokenKind.Integer:
                    ExpressionType = new ClrClassDefinition(typeof(int));
                    return true;
                case TokenKind.OpSingleQuote:
                    ExpressionType = new ClrClassDefinition(typeof(string));
                    return true;
                default:
                    throw new NotImplementedException();
                    return false;
            }

            return false;
        }
    }
}