using System.Collections.Generic;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class ExprResolverBase
    {
        private List<ExprResolverBase> _children = new List<ExprResolverBase>();

        public List<ExprResolverBase> Children
        {
            get { return _children; }
        }
    }
}