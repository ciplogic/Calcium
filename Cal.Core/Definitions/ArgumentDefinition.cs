using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions
{
    public class ArgumentDefinition : VariableDefinition
    {
        public VariableDefinition Variable { get; set; }
        public List<TokenDef> Tokens { get; set; }

        public ArgumentDefinition()
        {
            Variable = new VariableDefinition();
        }

        public void ProcessDefinition(List<TokenDef> byToken, ProgramDefinition programScope)
        {
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpOpenParen);
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpCloseParen);
            byToken.RemoveAll(tok => tok.Kind == TokenKind.OpColumn);
            Tokens = byToken;
            Variable.Name = Tokens[0].GetContent();
            Variable.Type = new ClassDefinition(programScope)
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