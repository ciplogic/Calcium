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

        public ExpressionDefinition(List<TokenDef> contentTokens, InstructionDefinition parentDefinition)
        {
            ChildrenExpressions =new List<ExpressionDefinition>();
            _contentTokens = contentTokens;
            ParentDefinition = parentDefinition;

            EvaluateExpression(contentTokens);
        }

        private void EvaluateExpression(List<TokenDef> contentTokens)
        {
            ExpressionResolver.Resolve(this);
            var unknownIdentifiers = contentTokens.Where(tok => tok.Kind == TokenKind.Identifier);
            for (var i = 0; i < contentTokens.Count; i++)
            {
                
            }
        }

        public List<TokenDef> ContentTokens
        {
            get { return _contentTokens; }
        }

        public ClassDefinition TypeDefinition { get; set; }
        public InstructionDefinition ParentDefinition { get; set; }

        public List<ExpressionDefinition> ChildrenExpressions { get; set; }

        public override string ToString()
        {
            return ContentTokens.TokenJoinContent(); 
        }

        public void WriteCode(StringBuilder sb)
        {
            sb.Append(ToString());
        }
    }
}