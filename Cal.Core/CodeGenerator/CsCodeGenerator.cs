using System.Collections.Generic;
using System.Text;
using Cal.Core.Definitions;
using Cal.Core.Utils;

namespace Cal.Core.CodeGenerator
{
    public class CsCodeGenerator
    {
        public const string MainCs = "main.cs";
        private readonly ProgramDefinition _definition;

        static readonly List<string> DefaultNamespaces = new List<string>
        {
            "System", 
            "System.Collections.Generic"
        };

        public bool MultiFile { get; set; }

        public CsCodeGenerator(ProgramDefinition definition)
        {
            _definition = definition;
        }

        public static void AddNamespace(string ns)
        {
            if(!DefaultNamespaces.Contains(ns))
                DefaultNamespaces.Add(ns);
        }

        public Dictionary<string, string> GenerateFilePack(bool multiFile)
        {
            var result = new Dictionary<string, string>();
            if (!multiFile)
            {
                string fullCode = GenerateAllAsOneFile(_definition);
                result[MainCs] = fullCode;
            }

            return result;
        }

        private string GenerateAllAsOneFile(ProgramDefinition definition)
        {
            var sb = new StringBuilder();
            DefaultNamespaces.Each(item
                =>sb.AppendFormat("using {0};",item).AppendLine());
            var classDefinitions = definition.Classes;
            foreach (var classDefinition in classDefinitions)
            {
                ClassCodeGenerator.GenerateClassCode(sb, classDefinition);
            }

            return sb.ToString();
        }
    }
}
