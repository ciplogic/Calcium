using System.Linq;
using System.Text;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class WhileDefinition : InstructionDefinition
    {
        public ExpressionDefinition WhileExpression { get; set; }
        public ScopeDefinition WhileBody { get; set; }

        public WhileDefinition(AstNode item)
        {
            WhileBody = new ScopeDefinition();
            WhileExpression = new ExpressionDefinition(item.ChildrenNodes[0].RowTokens.Items.Skip(1).ToList())
            {
                ParentDefinition = this
            };
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append("while(").Append(WhileExpression).AppendLine(")");
            WhileBody.WriteCode(sb);
        }
    }
}