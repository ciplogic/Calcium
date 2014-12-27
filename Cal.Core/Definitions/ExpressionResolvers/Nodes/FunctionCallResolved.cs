using System.Collections.Generic;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class FunctionCallResolved : ExprResolverBase
    {
        private readonly List<TokenDef> _contentTokens;
        public string FunctionName { get; set; }
        public List<ExprResolverBase> ArgumentCalls { get; private set; }

        public FunctionCallResolved(List<TokenDef> contentTokens, InstructionDefinition parentDefinition)
            : base(ExpressionKind.FunctionCall)
        {
            ArgumentCalls = new List<ExprResolverBase>();
            FunctionName = contentTokens[0].GetContent();
            _contentTokens = contentTokens;
            var argumentTokens = contentTokens.GetRange(2, contentTokens.Count - 3);
            var argumentTokensSplit = argumentTokens.SplitBlockByToken(TokenKind.OpComma);
            foreach (var tokenSplit in argumentTokensSplit)
            {
                var resolvedExpression = ExpressionResolver.Resolve(tokenSplit, parentDefinition);
                ArgumentCalls.Add(resolvedExpression);
            }
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }
    }
}