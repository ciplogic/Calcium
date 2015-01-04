using System;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    internal class BreakExpression : ExprResolverBase
    {
        public BreakExpression() : base(ExpressionKind.SpecialOperation)
        {
        }

        public override string ToCode()
        {
            return "break;";
        }

        public override bool CalculateExpressionType()
        {
            throw new InvalidOperationException();
            return false;

        }
    }
}