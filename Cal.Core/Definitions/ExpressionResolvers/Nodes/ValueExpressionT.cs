using System;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class ValueExpressionT<T> : ValueExpression
    {
        public ValueExpressionT(TokenDef contentToken, Func<string, T> convert) : base(contentToken)
        {
            Value = convert(contentToken.GetContent());
        }

        public T Value { get; private set; }
    }
}