using System;
using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions.IdentifierDefinition;
using Cal.Core.Definitions.ReferenceDefinitions;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class MethodDefinition : BlockDefinition
    {
        public MethodDefinition(BlockDefinition scope)
            : base(scope, "Def body", BlockKind.Method)
        {
            ParentScope = scope;
            Arguments = new List<ArgumentDefinition>();
            ReturnType = new ClrClassDefinition(typeof(void));
        }

        public string Name { get; set; }
        public ClassDefinition DeclaringType { get; set; }
        public ClassDefinition ReturnType { get; set; }
        public bool IsStatic { get; set; }
        public BlockDefinition ParentScope { get; private set; }
        public List<ArgumentDefinition> Arguments { get; set; } 


        public void ProcessMethodHeader(LineTokens firstRow)
        {
            Name = firstRow.Items[0].GetContent();
            if (firstRow.Count > 1)
            {
                ProcessArguments(firstRow.Items.GetRange(1, firstRow.Items.Count - 1));
            }
        }

        public override ReferenceValueDefinition LocateVariable(TokenDef tokenDef)
        {
            var argumentName = tokenDef.GetContent();
            var foundArgument = Arguments.FirstOrDefault(arg => arg.Name == argumentName);
            if (foundArgument != null)
            {
                return new ReferenceVariableDefinition(foundArgument);
            }
            return base.LocateVariable(tokenDef);
        }

        public override string ToString()
        {
            return String.Format("def {0}({1})", Name, String.Join(",",
                Arguments.Select(arg => arg.ComputedText())));
        }

        public void ProcessArguments(List<TokenDef> argumentTokens)
        {
            var splitByTokens = argumentTokens.SplitBlockByToken(TokenKind.OpComma);
            foreach (var byToken in splitByTokens)
            {
                var arg = new ArgumentDefinition();
                arg.ProcessDefinition(byToken);
                Arguments.Add(arg);
            }
        }

        public string CalculateArgumetsHeader()
        {
            var argumentData = new List<string>(
                    Arguments.Select(arg => arg.ComputedText())
                );
            return String.Join(", ", argumentData);
        }
    }
}