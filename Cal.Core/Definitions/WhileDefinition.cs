using System.Linq;
using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class WhileDefinition : InstructionDefinition
    {
        public ExprResolverBase WhileExpression { get; set; }
        public ScopeDefinition WhileBody { get; private set; }

        public WhileDefinition(AstNode item, ScopeDefinition scope)
            : base(scope)
        {
            WhileBody = new ScopeDefinition(scope, "While body");
            WhileExpression = ExpressionResolver.Resolve(item.ChildrenNodes[1].RowTokens.Items, this);
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append("while(").Append(WhileExpression.ToCode()).AppendLine(")");
            WhileBody.WriteCode(sb);
        }
    }
}