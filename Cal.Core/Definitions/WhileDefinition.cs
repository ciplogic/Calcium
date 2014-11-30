using System.Linq;
using System.Text;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class WhileDefinition : InstructionDefinition
    {
        public ExpressionDefinition WhileExpression { get; set; }
        public ScopeDefinition WhileBody { get; set; }

        public WhileDefinition(AstNode item, ScopeDefinition scope)
            : base(scope)
        {
            WhileBody = new ScopeDefinition()
            {
                ParentScope = scope
            };
            WhileExpression = new ExpressionDefinition(item.ChildrenNodes[0].RowTokens.Range(1), this)
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