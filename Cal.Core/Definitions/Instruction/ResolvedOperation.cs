using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;

namespace Cal.Core.Definitions.Instruction
{
    public class ResolvedOperation : InstructionDefinition
    {
        public ExprResolverBase Expression { get; private set; }

        public ResolvedOperation(ExprResolverBase expressionBase, BlockDefinition scope) : base(scope)
        {
            Expression = expressionBase;
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.AppendLine(Expression.ToCode());
        }
    }
}