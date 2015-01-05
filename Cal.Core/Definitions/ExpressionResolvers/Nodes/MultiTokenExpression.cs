using System;
using System.Collections.Generic;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    internal class MultiTokenExpression : ExprResolverBase
    {
        private readonly List<TokenDef> _contentTokens;

        public MultiTokenExpression(List<TokenDef> contentTokens) : base(ExpressionKind.MultiToken)
        {
            _contentTokens = contentTokens;
        }

        public override string ToCode()
        {
            return _contentTokens.TokenJoinContent();
        }

        public override bool CalculateExpressionType()
        {
            this.ExpressionType = new ClrClassDefinition(typeof(int));
            return true;
        }
    }
}