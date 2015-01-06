using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.CodeGenerator;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Definitions.Instruction;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class ScopeDefinition : BaseDefinition
    {
        public string Name { get; set; }
        public List<InstructionDefinition> Operations { get; set; }
        public readonly List<VariableDefinition> Variables = new List<VariableDefinition>();


        public ScopeDefinition(string name)
        {
            Operations = new List<InstructionDefinition>();
            Name = name;
        }

        public void ProcessAddCall(AstNode item, BlockDefinition scope, ExprResolverBase instructionResolver)
        {
            Operations.Add(new CallDefinition(item.RowTokens.Items, scope, instructionResolver));
        }

        public void ProcessAddOperation(InstructionDefinition operation)
        {
            Operations.Add(operation);
        }


        public VariableDefinition LocateVariable(string name)
        {
            var varWithName = Variables.FirstOrDefault(vr => vr.Name == name);
            return varWithName;
        }


        public bool AddVariable(string variableName)
        {
            var existingVar = Variables.FirstOrDefault(v => v.Name == variableName);
            if (existingVar != null)
                return false;
            var variableDefinition = new VariableDefinition
            {
                Name = variableName
            };
            Variables.Add(variableDefinition);
            return true;
        }

        public void WriteCode(StringBuilder sb)
        {
            sb.AppendLine("{");
            DefCodeGenerator.GenerateVariablesCode(sb, Variables);
            foreach (var instructionDefinition in Operations)
            {
                instructionDefinition.WriteCode(sb);
            }
            sb.AppendLine("}");
        }

        public void AddResolvedOperation(ExprResolverBase instructionResolver, BlockDefinition scope)
        {
            Operations.Add(new ResolvedOperation(instructionResolver,scope));
        }
    }
}