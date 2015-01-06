using System.Text;
using Cal.Core.Definitions.Instruction;

namespace Cal.Core.Definitions
{
    internal class ReturnVariable : InstructionDefinition
    {
        public VariableDefinition Variable { get; set; }

        public ReturnVariable(VariableDefinition variable, BlockDefinition scope) : base(scope)
        {
            Variable = variable;
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.AppendFormat("return {0};", Variable.Name)
              .AppendLine();
        }
    }
}