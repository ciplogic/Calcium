using System;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class ValueExpressionDouble : ValueExpressionT<double>
    {
        public ValueExpressionDouble(TokenDef contentToken) : base(contentToken)
        {
            Value = Convert.ToDouble(contentToken.GetContent());
        }
    }
}