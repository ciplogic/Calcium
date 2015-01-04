using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;

namespace Cal.Core.Definitions
{
    public class ResolvedOperation : InstructionDefinition
    {
        private readonly ExprResolverBase _exprResolverBase;

        public ResolvedOperation(ExprResolverBase exprResolverBase, BlockDefinition scope) : base(scope)
        {
            _exprResolverBase = exprResolverBase;
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.AppendLine(_exprResolverBase.ToCode());
        }
    }
}