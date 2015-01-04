using System;
using System.Collections.Generic;
using Cal.Core.Definitions.IdentifierDefinition;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions.Assigns
{
    public class AssignLeftDefinition 
    {
        private readonly List<TokenDef> _tokens;
        public ReferenceValueDefinition ReferenceDefinition { get; set; }

        public AssignLeftDefinition(List<TokenDef> tokens, BlockDefinition instructionScope)
        {
            _tokens = tokens;
            ReferenceDefinition = ReferenceResolver.Resolve(tokens, instructionScope);
            if (ReferenceDefinition.Definition == null && tokens.Count==1)
            {
                var firstToken = tokens[0];
                instructionScope.AddVariable(firstToken);
            }
            ReferenceDefinition = ReferenceResolver.Resolve(tokens, instructionScope);
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

        public override string ToString()
        {
            return _tokens.TokenJoinContent();
        }
    }
}