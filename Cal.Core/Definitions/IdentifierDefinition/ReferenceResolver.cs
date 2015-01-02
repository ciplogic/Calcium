using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cal.Core.Lexer;
using Cal.Core.Runtime;

namespace Cal.Core.Definitions.IdentifierDefinition
{
    public class ReferenceResolver
    {
        public Dictionary<string, List<ReferenceValueDefinition>> DefinitionDictionary { get; set; } 
        public static ReferenceResolver Instance
        {
            get { return StaticInstance; }
        }

        ReferenceResolver()
        {
            DefinitionDictionary = new Dictionary<string, List<ReferenceValueDefinition>>();
        }
        static readonly ReferenceResolver StaticInstance =new ReferenceResolver();

        public static ReferenceValueDefinition Resolve(List<TokenDef> tokens, BlockDefinition parentScope)
        {
            return Instance.ResolveInstance(tokens, parentScope);
        }

        private ReferenceValueDefinition ResolveInstance(List<TokenDef> tokens, BlockDefinition parentScope)
        {
            throw new NotImplementedException();
        }

        public void ScanGlobalMethods(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var customAttr = type.GetCustomAttributeT<GlobalFunctions>();
                if (customAttr == null)
                    continue;
                ScanTypeWithGlobalMethods(type);
            }
        }

        private void ScanTypeWithGlobalMethods(Type type)
        {
            foreach (var methodInfo in type.GetMethods())
            {
                AddMethod(methodInfo);
            }
        }

        private void AddMethod(MethodInfo methodInfo)
        {
            var referencesList = DefinitionDictionary.SetItem(methodInfo.Name);
            referencesList.Add(new MethodReferenceDefinition(methodInfo));
        }
    }
}
