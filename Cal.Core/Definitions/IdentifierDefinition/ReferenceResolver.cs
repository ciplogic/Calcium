using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.IdentifierDefinition
{
    public class ReferenceResolver
    {
        public static ReferenceResolver Instance
        {
            get { return StaticInstance; }
        }

        ReferenceResolver()
        {
            
        }
        static readonly ReferenceResolver StaticInstance =new ReferenceResolver();

        public static ReferenceValueDefinition Resolve(List<TokenDef> tokens, ScopeDefinition parentScope)
        {
            return Instance.ResolveInstance(tokens, parentScope);
        }

        private ReferenceValueDefinition ResolveInstance(List<TokenDef> tokens, ScopeDefinition parentScope)
        {
            throw new NotImplementedException();
        }

        public void ScanGlobalMethods(Assembly assembly)
        {
            
        }
    }
}
