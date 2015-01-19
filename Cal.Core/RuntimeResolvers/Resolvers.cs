using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions;
using Cal.Core.Lexer;

namespace Cal.Core.RuntimeResolvers
{
    public class Resolvers
    {
        public static Resolvers Instance
        {
            get { return StaticInstance; }
        }

        public static Resolvers StaticInstance = new Resolvers();
        List<RuntimeResolverBase> resolverBases = new List<RuntimeResolverBase>(); 
        private Resolvers()
        {
            resolverBases.Add(new CalRuntimeResolver());
            resolverBases.Add(new CurrentClassResolver());
        }

        public RuntimeResolverBase GetFunctionResolver(List<TokenDef> contentTokens, BlockDefinition instructionDefinition)
        {
            return resolverBases.FirstOrDefault(res => res.CanResolveFunction(contentTokens, instructionDefinition));
        }
    }
}