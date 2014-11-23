using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions
{
    public class ArgumentDefinition
    {
        public VariableDefinition Variable { get; set; }
        public List<TokenDef> Tokens { get; set; }

        public ArgumentDefinition()
        {
            Variable = new VariableDefinition();
        }

        public void ProcessDefinition(List<TokenDef> byToken)
        {
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpOpenParen);
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpCloseParen);
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpColumn);
            Tokens = byToken;
            Variable.Name = Tokens[0].GetContent();
            Variable.Type = new ClassDefinition()
            {
                Name = Tokens[1].GetContent()
            };
        }

        public override string ToString()
        {
            return Variable.ToString();
        }

        public string ComputedText()
        {
            return string.Format("{0} {1}", Variable.Type.Name, Variable.Name);
        }
    }
}