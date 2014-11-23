using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;
using Cal.Core.Semantic;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class MethodDefinition : BaseDefinition
    {
        public MethodDefinition()
        {
            ReturnType = new ClrClassDefinition(typeof(void));
            MainBody = new BlockDefinition();
        }

        public string Name { get; set; }
        public ClassDefinition DeclaringType { get; set; }
        public List<ArgumentDefinition> Arguments = new List<ArgumentDefinition>();
        public ClassDefinition ReturnType { get; set; }
        public BlockDefinition MainBody { get; set; }
        public bool IsStatic { get; set; }


        public void ProcessAssign(AstNode item)
        {
            List<TokenKind> tokenKinds = item.RowTokens.Items
                .Select(tok=>tok.Kind)
                .ToList();
            var indexAssignOp = tokenKinds
                .IndexOf(TokenKind.OpAssign);
            var assignDefinition = new AssignDefinition();
            var leftTokens = item.RowTokens.Items.GetRange(0, indexAssignOp );
            var rightTOkens = item.RowTokens.Items.GetRange(indexAssignOp + 1, tokenKinds.Count - indexAssignOp - 1);
            assignDefinition.Left = new AssignLeftDefinition(leftTokens);
            assignDefinition.RightExpression= new ExpressionDefinition(rightTOkens);

            AddInstruction(assignDefinition);
        }

        private void AddInstruction(AssignDefinition assignDefinition)
        {
            MainBody.Scope.Operations.Add(assignDefinition);
            SemanticAnalysis.AnalyseFirstAssign(this, assignDefinition, this.MainBody.Scope);
        }

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