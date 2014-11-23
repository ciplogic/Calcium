using System.Text;

namespace Cal.Core.Definitions
{
    public class InstructionDefinition
    {
        public virtual void WriteCode(StringBuilder sb)
        {
            sb.AppendLine("//not implemented");
        }
    }
}