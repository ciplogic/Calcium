using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class ParenResolved : ExprResolverBase
    {
        public ExprResolverBase InnerExpression { get; set; }

        public ParenResolved(List<TokenDef> contentTokens, BlockDefinition instructionDefinition)
            : base(ExpressionKind.Parentheses)
        {
            InnerExpression = ExpressionResolver.Resolve(contentTokens, instructionDefinition);
        }

        public override string ToCode()
        {
            return string.Format("({0})", InnerExpression.ToCode());
        }

        public override bool CalculateExpressionType()
        {
            if (!InnerExpression.CalculateExpressionType())
                return false;
            ExpressionType = InnerExpression.ExpressionType;
            return true;
        }
    }
}