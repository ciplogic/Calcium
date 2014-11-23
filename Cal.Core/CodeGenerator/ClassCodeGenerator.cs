using System.Text;
using Cal.Core.Definitions;

namespace Cal.Core.CodeGenerator
{
    public static class ClassCodeGenerator
    {
        public static void GenerateClassCode(StringBuilder sb, ClassDefinition classDefinition)
        {
            if (classDefinition.IsClrType)
                return;
            sb.AppendFormat("public class {0}", classDefinition.Name);
            if (classDefinition.BaseType != null)
            {
                sb.AppendFormat(": {0}", classDefinition.BaseType.Name);
            }
            sb.AppendLine(" {").AppendLine();
            foreach (var definition in classDefinition.Defs)
            {
                DefCodeGenerator.GenerateDefinitionCode(sb, definition);
            }

            sb.AppendLine("}");
        }
    }
}