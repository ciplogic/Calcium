namespace Cal.Core.Definitions.ExpressionResolvers
{
    public abstract class ExprResolverBase
    {
        public ExpressionKind Kind { get; private set; }
        public ClassDefinition ExpressionType { get; set; }

        public ExprResolverBase(ExpressionKind kind)
        {
            Kind = kind;
        }

        public abstract string ToCode();
        public abstract bool CalculateExpressionType();
    }
}