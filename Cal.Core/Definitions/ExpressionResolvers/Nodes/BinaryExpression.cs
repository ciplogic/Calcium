using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    internal class BinaryExpression : ExprResolverBase
    {
        public ExprResolverBase LeftDefinition { get; private set; }
        public TokenDef TokenDef { get; set; }
        public ExprResolverBase RightDefinition { get; private set; }

        public BinaryExpression(TokenDef tokenDef, List<TokenDef> leftTokens,List<TokenDef> rightTokens,
            InstructionDefinition parentExpression) 
            : base(ExpressionKind.BunaryOperator)
        {
            TokenDef = tokenDef;
            LeftDefinition = ExpressionResolver.Resolve(leftTokens, parentExpression);
            RightDefinition = ExpressionResolver.Resolve(rightTokens, parentExpression);
        }

        public override string ToCode()
        {
            return string.Format("{0}{1}{2}", LeftDefinition.ToCode(), TokenDef.GetContent(), RightDefinition.ToCode());
        }
    }
}