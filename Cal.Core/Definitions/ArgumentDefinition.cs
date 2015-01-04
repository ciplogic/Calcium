using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions
{
    public class ArgumentDefinition : VariableDefinition
    {
        public List<TokenDef> Tokens { get; set; }

        public ArgumentDefinition()
        {
            Kind = VariableKind.Argument;
        }

        public void ProcessDefinition(List<TokenDef> byToken)
        {
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpOpenParen);
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpCloseParen);
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpColumn);
            Tokens = byToken;
            Name = Tokens[0].GetContent();
            Type = new ClassDefinition(ProgramDefinition.Instance)
            {
                Name = Tokens[1].GetContent()
            };
        }

        public string ComputedText()
        {
            return string.Format("{0} {1}", Type.Name, Name);
        }
    }
}