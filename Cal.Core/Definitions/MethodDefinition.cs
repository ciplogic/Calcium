using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class MethodDefinition : BaseDefinition
    {
        public MethodDefinition()
        {
            ReturnType = new ClrClassDefinition(typeof(void));
            MainBody = new BlockDefinition {
                Scope =
                {
                    Parent = this
                }
            };
        }

        public string Name { get; set; }
        public ClassDefinition DeclaringType { get; set; }
        public List<ArgumentDefinition> Arguments = new List<ArgumentDefinition>();
        public ClassDefinition ReturnType { get; set; }
        public BlockDefinition MainBody { get; set; }
        public bool IsStatic { get; set; }



        public override string ToString()
        {
            return string.Format("def {0}({1})", Name, string.Join(",", Arguments));
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
                    Arguments.Select(arg=>arg.ComputedText())
                );
            return string.Join(", ", argumentData);
        }
    }
}