using System;
using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class MethodDefinition : BlockDefinition
    {
        public MethodDefinition(BlockDefinition scope)
            : base(scope.Scope, "Def body", BlockKind.Method)
        {
            ParentScope = scope;
            ReturnType = new ClrClassDefinition(typeof(void));
        }

        public string Name { get; set; }
        public ClassDefinition DeclaringType { get; set; }
        public ClassDefinition ReturnType { get; set; }
        public bool IsStatic { get; set; }
        public BlockDefinition ParentScope { get; private set; }


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
            return String.Format("def {0}({1})", Name, String.Join(",", Scope.Variables));
        }

        public void ProcessArguments(List<TokenDef> argumentTokens)
        {
            var splitByTokens = argumentTokens.SplitBlockByToken(TokenKind.OpComma);
            foreach (var byToken in splitByTokens)
            {
                var arg = new ArgumentDefinition();
                arg.ProcessDefinition(byToken, DeclaringType.ProgramScope);
                Scope.AddVariable(arg.Variable);
            }
        }

        public string CalculateArgumetsHeader()
        {
            var argumentData = new List<string>(
                    Scope.Variables.Select(arg => ((ArgumentDefinition)arg).ComputedText())
                );
            return String.Join(", ", argumentData);
        }
    }
}