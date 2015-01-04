using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class ExpressionDefinition
    {
        private readonly List<TokenDef> _contentTokens;
       
        public List<TokenDef> ContentTokens
        {
            get { return _contentTokens; }
        }

        public ClassDefinition TypeDefinition { get; set; }
       
        public override string ToString()
        {
            return ContentTokens.TokenJoinContent(); 
        }

           }
}