namespace Cal.Core.Definitions.ExpressionResolvers
{
    public abstract class ExprResolverBase
    {
        public ExpressionKind Kind { get; private set; }

        public ExprResolverBase(ExpressionKind kind)
        {
            Kind = kind;
        }

        public abstract string ToCode();
    }
}