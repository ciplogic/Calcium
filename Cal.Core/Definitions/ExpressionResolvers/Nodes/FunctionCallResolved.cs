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
        public string FunctionName {
            get
            {
                if (string.IsNullOrEmpty(ClassName))
                    return MethodName;
                return string.Format("{0}.{1}", ClassName, MethodName);
            }
        }
        public List<ExprResolverBase> ArgumentCalls { get; private set; }

        public ReferenceFunctionDefinition FunctionDefinition { get; set; }

        public string MethodName { get; set; }
        public string ClassName { get; set; }

        public FunctionCallResolved(List<TokenDef> contentTokens, BlockDefinition parentDefinition)
            : base(ExpressionKind.FunctionCall)
        {
            ArgumentCalls = new List<ExprResolverBase>();
            MethodName = contentTokens[0].GetContent();
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
            MethodName = MethodDefinition.Info.Name;
            ClassName = MethodDefinition.Info.DeclaringType.Name;
        }


        public FunctionCallResolved(MethodDefinition methodDefinition) : base(ExpressionKind.FunctionCall)
        {
            FunctionDefinition = new ReferenceFunctionDefinition
            {
                Definition = methodDefinition
            };
            MethodName = methodDefinition.Name;
            ClassName = ((ClassDefinition) methodDefinition.Parent).Name;
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