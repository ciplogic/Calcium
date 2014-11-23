using System.Collections.Generic;
using System.Linq;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class ScopeDefinition : BaseDefinition
    {
        public ScopeDefinition ParentScope { get; set; }
        public List<InstructionDefinition> Operations { get; set; }

        public List<VariableDefinition> Variables = new List<VariableDefinition>();

        public ScopeDefinition()
        {
            Operations = new List<InstructionDefinition>();
        }



        public void ProcessAddCall(AstNode item)
        {
            Operations.Add(new CallDefinition(item.RowTokens.Items));
        }


        public VariableDefinition LocateVariable(string name)
        {
            var varWithName = Variables.FirstOrDefault(vr => vr.Name == name);
            if (varWithName != null)
                return varWithName;
            if (ParentScope == null)
                return null;
            return ParentScope.LocateVariable(name);
        }
    }
}