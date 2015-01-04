using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    internal class ExprUnaryResolved: ExprResolverBase
    {
        private readonly TokenDef _tokenDef;

        public ExprUnaryResolved(TokenDef tokenDef, List<TokenDef> remainingTokens, BlockDefinition expressionDefinition)
            : base(ExpressionKind.UnaryOperator)
        {
            _tokenDef = tokenDef;
            RightDefinition = ExpressionResolver.Resolve(remainingTokens, expressionDefinition);
        }

        public TokenDef TokenDef
        {
            get { return _tokenDef; }
        }
        public ExprResolverBase RightDefinition { get; private set; }

        public override string ToCode()
        {
            return string.Format("{0} {1}", TokenDef.GetContent(), RightDefinition.ToCode());
        }

        public override bool CalculateExpressionType()
        {
            if (!RightDefinition.CalculateExpressionType())
                return false;
            ExpressionType = RightDefinition.ExpressionType;
            return true;
        }
    }
}