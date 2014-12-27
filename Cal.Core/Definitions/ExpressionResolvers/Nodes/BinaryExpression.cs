using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    internal class BinaryExpression : ExprResolverBase
    {
        private readonly InstructionDefinition _parentExpression;
        public ExprResolverBase LeftDefinition { get; private set; }
        public TokenDef TokenDef { get; set; }
        public ExprResolverBase RightDefinition { get; private set; }

        public BinaryExpression(TokenDef tokenDef, List<TokenDef> leftTokens,List<TokenDef> rightTokens,
            InstructionDefinition parentExpression) 
            : base(ExpressionKind.BunaryOperator)
        {
            _parentExpression = parentExpression;
            TokenDef = tokenDef;
            LeftDefinition = ExpressionResolver.Resolve(leftTokens, parentExpression);
            RightDefinition = ExpressionResolver.Resolve(rightTokens, parentExpression);
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }
    }
}