﻿using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class ValueExpressionT<T> : ValueExpression
    {
        public ValueExpressionT(TokenDef contentToken) : base(contentToken)
        {
        }

        public T Value { get; set; }
    }
}