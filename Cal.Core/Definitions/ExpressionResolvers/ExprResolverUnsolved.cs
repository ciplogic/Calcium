using System;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class ExprResolverUnsolved : ExprResolverBase
    {
        public ExprResolverUnsolved() : base(ExpressionKind.Unknown)
        {
        }

        public override string ToCode()
        {
            throw new NotImplementedException();
        }

        public override bool CalculateExpressionType()
        {
            throw new NotImplementedException();
        }
    }
}