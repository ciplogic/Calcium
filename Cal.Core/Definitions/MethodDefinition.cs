using System;
using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class MethodDefinition : BaseDefinition
    {
        public MethodDefinition(ScopeDefinition scope)
        {
            ReturnType = new ClrClassDefinition(typeof(void));
            MethodScope = new ScopeDefinition(scope, "Def scope");
            MainBody = new BlockDefinition(MethodScope, "Def body");
        }

        public string Name { get; set; }
        public ClassDefinition DeclaringType { get; set; }
        public ScopeDefinition MethodScope { get; private set; }
        public ClassDefinition ReturnType { get; set; }
        public BlockDefinition MainBody { get; set; }
        public bool IsStatic { get; set; }


        public void ProcessMethodHeader(LineTokens firstRow)
        {
            Name = firstRow.Items[0].GetContent();
            if (firstRow.Count > 1)
            {
                ProcessArguments(firstRow.Items.GetRange(1, firstRow.Items.Count - 1));
            }
        }

        public override string ToString()
        {
            return String.Format("def {0}({1})", Name, String.Join(",", MethodScope.Variables));
        }

        public void ProcessArguments(List<TokenDef> argumentTokens)
        {
            var splitByTokens = argumentTokens.SplitBlockByToken(TokenKind.OpComma);
            foreach (var byToken in splitByTokens)
            {
                var arg = new ArgumentDefinition();
                arg.ProcessDefinition(byToken);
                MethodScope.Variables.Add(arg);
                MainBody.Scope.AddVariable(arg.Variable);
            }
        }

        public string CalculateArgumetsHeader()
        {
            var argumentData = new List<string>(
                    MethodScope.Variables.Select(arg => ((ArgumentDefinition)arg).ComputedText())
                );
            return String.Join(", ", argumentData);
        }
    }
}