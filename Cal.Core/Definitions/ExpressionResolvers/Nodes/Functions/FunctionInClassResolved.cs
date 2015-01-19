namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class FunctionInClassResolved : ExprResolverBase
    {
        public FunctionInClassResolved()
            : base(ExpressionKind.FunctionCall)
        {
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }

        public override bool CalculateExpressionType()
        {
            throw new System.NotImplementedException();
        }
    }
}