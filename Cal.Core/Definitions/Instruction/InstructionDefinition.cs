using System;
using System.Text;

namespace Cal.Core.Definitions.Instruction
{
    public class InstructionDefinition
    {
        public BlockDefinition ParentBlock { get; set; }

        public InstructionDefinition(BlockDefinition scope)
        {
            ParentBlock = scope;
        }

        public virtual void WriteCode(StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}