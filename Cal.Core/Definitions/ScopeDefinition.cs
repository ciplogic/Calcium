using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cal.Core.CodeGenerator;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Lexer;
using Cal.Core.Semantic;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class ScopeDefinition : BaseDefinition
    {
        public ScopeDefinition ParentScope { get; set; }
        public string Name { get; set; }
        public List<InstructionDefinition> Operations { get; set; }

        public MethodDefinition Method
        {
            get
            {
                var root = this;
                while (root.ParentScope!=null)
                {
                    root = root.ParentScope;
                }
                return (MethodDefinition) root.Parent;
            }
        }

        public readonly List<VariableDefinition> Variables = new List<VariableDefinition>();

        public ScopeDefinition(ScopeDefinition scope, string name)
        {
            Operations = new List<InstructionDefinition>();
            ParentScope = scope;
            Name = name;
        }

        public void ProcessAddCall(AstNode item, ScopeDefinition scope)
        {
            Operations.Add(new CallDefinition(item.RowTokens.Items, scope));
        }
        public void ProcessAddOperation(AstNode item, InstructionDefinition operation)
        {
            Operations.Add(operation);
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

        public void ProcessAssign(AstNode item, ScopeDefinition scope)
        {
            List<TokenKind> tokenKinds = item.RowTokens.Items
                .Select(tok => tok.Kind)
                .ToList();
            var indexAssignOp = tokenKinds
                .IndexOf(TokenKind.OpAssign);
            var assignDefinition = new AssignDefinition(scope);
            var leftTokens = item.RowTokens.Items.GetRange(0, indexAssignOp);
            var rightTOkens = item.RowTokens.Items.GetRange(indexAssignOp + 1, tokenKinds.Count - indexAssignOp - 1);
            assignDefinition.Left = new AssignLeftDefinition(leftTokens);
            assignDefinition.RightExpression = ExpressionResolver.Resolve(rightTOkens, assignDefinition);
            AddLeftIfOneToken(assignDefinition.Left);
            AddInstruction(assignDefinition);
        }

        private void AddLeftIfOneToken(AssignLeftDefinition left)
        {
            if(left.Tokens.Count!=1)
                return;
            var firstToken = left.Tokens[0];
            AddVariable(firstToken.GetContent());
        }

        private void AddInstruction(AssignDefinition assignDefinition)
        {
            Operations.Add(assignDefinition);
            SemanticAnalysis.AnalyseFirstAssign(assignDefinition, this);
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

        public void AddVariable(VariableDefinition variableName)
        {
            var existingVar = Variables.FirstOrDefault(v => v.Name == variableName.Name);
            if(existingVar!=null)
                throw new InvalidDataException("Argument should be defined first time");
            Variables.Add(variableName);
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


    }
}