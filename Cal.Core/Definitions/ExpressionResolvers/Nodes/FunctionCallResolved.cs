using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class FunctionCallResolved : ExprResolverBase
    {
        public string FunctionName { get; set; }
        public List<ExprResolverBase> ArgumentCalls { get; private set; }

        public FunctionCallResolved(List<TokenDef> contentTokens, InstructionDefinition parentDefinition)
            : base(ExpressionKind.FunctionCall)
        {
            ArgumentCalls = new List<ExprResolverBase>();
            FunctionName = contentTokens[0].GetContent();
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
            return
                string.Format("{0}({1})", FunctionName,
                    string.Join(",", ArgumentCalls.Select(arg => arg.ToCode()))
                    );
        }
    }
}