using System.Collections.Generic;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public abstract class ExprResolverBase
    {
        public ExpressionKind Kind { get; private set; }

        public ExprResolverBase(ExpressionKind kind)
        {
            Kind = kind;
        }
        private List<ExprResolverBase> _children = new List<ExprResolverBase>();

        public List<ExprResolverBase> Children
        {
            get { return _children; }
        }

        public abstract string ToCode();
    }
}