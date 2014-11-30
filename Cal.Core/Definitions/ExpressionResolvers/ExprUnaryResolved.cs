using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    internal class ExprBinaryResolved : ExprResolverBase
    {
        private readonly ExpressionDefinition _parentExpression;
        public ExprResolverBase LeftDefinition { get; private set; }
        public TokenDef TokenDef { get; set; }
        public ExprResolverBase RightDefinition { get; private set; }

        public ExprBinaryResolved(TokenDef tokenDef, List<TokenDef> leftTokens,List<TokenDef> rightTokens,
            ExpressionDefinition parentExpression)
        {
            _parentExpression = parentExpression;
            var leftExpression = new ExpressionDefinition(leftTokens, _parentExpression.ParentDefinition);
            var rightExpression = new ExpressionDefinition(rightTokens, _parentExpression.ParentDefinition);

            LeftDefinition = ExpressionResolver.Resolve(leftExpression);
            RightDefinition = ExpressionResolver.Resolve(rightExpression);
        }
    }

    internal class ExprUnaryResolved: ExprResolverBase
    {
        private readonly TokenDef _tokenDef;
        private ExpressionDefinition _parentExpression;

        public ExprUnaryResolved(TokenDef tokenDef, List<TokenDef> remainingTokens, ExpressionDefinition expressionDefinition)
        {
            _parentExpression = expressionDefinition;
            _tokenDef = tokenDef;
            var rightExpression = new ExpressionDefinition(remainingTokens, _parentExpression.ParentDefinition);
            RightDefinition = ExpressionResolver.Resolve(rightExpression);
        }

        public TokenDef TokenDef
        {
            get { return _tokenDef; }
        }
        public ExprResolverBase RightDefinition { get; private set; }

    }
}