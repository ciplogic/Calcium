using System.Text;

namespace Cal.Core.Definitions
{
    public class AssignDefinition : InstructionDefinition
    {
        public AssignLeftDefinition Left { get; set; }

        public ExpressionDefinition RightExpression { get; set; }

        public override string ToString()
        {
            return string.Format("{0} = {1}", Left, RightExpression);
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.AppendFormat("{0} = {1};", Left, RightExpression)
                .AppendLine();
        }
    }
}