using System;
using System.Collections.Generic;
using Cal.Core.Definitions.IdentifierDefinition;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions
{
    public class AssignLeftDefinition 
    {
        private readonly List<TokenDef> _tokens;
        public ReferenceValueDefinition ReferenceDefinition { get; set; }

        public AssignLeftDefinition(List<TokenDef> tokens, ScopeDefinition parentScope)
        {
            ReferenceDefinition = ReferenceResolver.Resolve(tokens, parentScope);
            _tokens = tokens;
        }

        public List<TokenDef> Tokens
        {
            get { return _tokens; }
        }

        public string ToCode()
        {
            if (Tokens.Count == 1)
            {
                return Tokens[0].GetContent();
            }
            throw new NotImplementedException();
        }
    }
}