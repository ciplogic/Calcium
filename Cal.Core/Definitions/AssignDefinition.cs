using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;

namespace Cal.Core.Definitions
{
    public class AssignDefinition : InstructionDefinition
    {
        public AssignDefinition(ScopeDefinition scope) : base(scope)
        {
        }

        public AssignLeftDefinition Left { get; set; }

        public ExprResolverBase RightExpression { get; set; }

        public override string ToString()
        {
            return string.Format("{0} = {1}", Left, RightExpression);
        }

        public override void WriteCode(StringBuilder sb)
        {
            var leftCode = Left.ToCode();
            sb.AppendFormat("{0} = {1};", leftCode, RightExpression.ToCode())
                .AppendLine();
        }
    }
}