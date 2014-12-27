using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class ParenResolved : ExprResolverBase
    {
        private readonly List<TokenDef> _contentTokens;

        public ParenResolved(List<TokenDef> contentTokens)
            : base(ExpressionKind.Parentheses)
        {
            _contentTokens = contentTokens;
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }
    }
}