using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class WhileDefinition : InstructionDefinition
    {
        public ExprResolverBase WhileExpression { get; set; }
        public BlockDefinition WhileBody { get; private set; }

        public WhileDefinition(AstNode item, BlockDefinition scope)
            : base(scope)
        {
            WhileBody = new BlockDefinition(scope, "While body",BlockKind.Instruction);
            WhileExpression = ExpressionResolver.Resolve(item.ChildrenNodes[1].RowTokens.Items, this);
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append("while(").Append(WhileExpression.ToCode()).AppendLine(")");
            WhileBody.Scope.WriteCode(sb);
        }
    }
}