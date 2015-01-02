using System;
using System.Collections.Generic;
using System.Text;
using Cal.Core.Definitions;

namespace Cal.Core.CodeGenerator
{
    public static class DefCodeGenerator
    {
        public static void WriteCodeBody(StringBuilder sb, MethodDefinition methodDefinition)
        {
            var operations = methodDefinition.Scope.Operations;
            foreach (var instructionDefinition in operations)
            {
                instructionDefinition.WriteCode(sb);
            }
        }

        public static void GenerateVariablesCode(StringBuilder sb, List<VariableDefinition> variables)
        {
            foreach (var variableDefinition in variables)
            {
                if (variableDefinition.Type == null)
                {
                    continue;
                }
                sb.AppendFormat("{0} {1};", variableDefinition.Type.Name, variableDefinition.Name);
                sb.AppendLine();
            }
        }

        private static string CalculateParameterNames(MethodDefinition definition)
        {
            if (definition.Scope.Variables.Count == 0)
            {
                return String.Empty;
            }
            return definition.CalculateArgumetsHeader();

        }

        public static void GenerateDefinitionCode(StringBuilder sb, MethodDefinition methodDefinition)
        {
            var returnTypeName = "void";
            if (methodDefinition.ReturnType != null)
            {
                returnTypeName = methodDefinition.ReturnType.Name;
            }
            if (methodDefinition.IsStatic)
            {
                sb.Append("static ");
            }
            sb.AppendFormat("public {0} {1} ({2})", returnTypeName, methodDefinition.Name,
                CalculateParameterNames(methodDefinition))
                .AppendLine().AppendLine("{");
            GenerateVariablesCode(sb, methodDefinition.Scope.Variables);
            WriteCodeBody(sb, methodDefinition);
            sb.AppendLine("}");
        }
    }
}