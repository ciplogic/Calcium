using System;
using System.Text;

namespace Cal.Core.Definitions
{
    public class InstructionDefinition
    {
        public ScopeDefinition Scope { get; set; }

        public InstructionDefinition(ScopeDefinition scope)
        {
            Scope = scope;
        }

        public virtual void WriteCode(StringBuilder sb)
        {
            throw new NotImplementedException();
            sb.AppendLine("//not implemented");
        }
    }
}