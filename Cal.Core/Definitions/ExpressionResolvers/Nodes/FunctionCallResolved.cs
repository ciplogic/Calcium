using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions.IdentifierDefinition;
using Cal.Core.Definitions.ReferenceDefinitions;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class FunctionCallResolved : ExprResolverBase
    {
        public MethodReferenceDefinition MethodDefinition { get; set; }
        public string FunctionName { get; set; }
        public List<ExprResolverBase> ArgumentCalls { get; private set; }

        public ReferenceFunctionDefinition FunctionDefinition { get; set; }

        public string MethodName
        {
            get
            {
                var info = MethodDefinition.Info;
                return string.Format("{0}.{1}", info.DeclaringType.Name, info.Name);
            }
        }

        public FunctionCallResolved(List<TokenDef> contentTokens, BlockDefinition parentDefinition)
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

        public FunctionCallResolved(MethodReferenceDefinition methodDefinition) : base(ExpressionKind.FunctionCall)
        {
            MethodDefinition = methodDefinition;
        }

        public override string ToCode()
        {
            return
                string.Format("{0}({1})", FunctionName,
                    string.Join(",", ArgumentCalls.Select(arg => arg.ToCode()))
                    );
        }

        public override bool CalculateExpressionType()
        {
            if (FunctionDefinition == null)
                return false;
            ExpressionType = FunctionDefinition.ReturnType;
            return true;
        }
    }
}