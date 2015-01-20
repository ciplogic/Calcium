using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers.Nodes
{
    public class RubyRuntimeFunctionResolved : ExprResolverBase
    {
        List<TokenDef> Arguments = new List<TokenDef>();
        static Dictionary<string, string> RubyFunctions = new Dictionary<string, string>()
        {
            {"puts", "Console.WriteLine"},
            {"print", "Console.Write"},
        };
        public RubyRuntimeFunctionResolved(List<TokenDef> contentTokens) : base(ExpressionKind.FunctionCall)
        {
            ProcessArguments(contentTokens);
        }

        private void ProcessArguments(List<TokenDef> contentTokens)
        {
            FunctionName = contentTokens[0].GetContent();
            for (var i = 1; i < contentTokens.Count; i++)
            {
                Arguments.Add(contentTokens[i]);
            }
        }

        public string FunctionName { get; set; }

        public override string ToCode()
        {
            var args = string.Join(", ", Arguments.Select(arg => arg.GetContent()));
            var runtimeName = RubyFunctions[FunctionName];
            var code = string.Format("{0}({1});"
                , runtimeName
                , args);

            return code;
        }

        public override bool CalculateExpressionType()
        {
            throw new System.NotImplementedException();
        }
    }
}