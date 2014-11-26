using System.CodeDom.Compiler;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class WhileDefinition : InstructionDefinition
    {
        private readonly AstNode _item;
        public ExpressionDefinition whileExpression;

        public WhileDefinition(AstNode item)
        {
            _item = item;
            WhileBody = new ScopeDefinition();
            whileExpression= new ExpressionDefinition(item.RowTokens.Items);
        }

        public ScopeDefinition WhileBody { get; set; }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append("while(");
            sb.Append(whileExpression);
            sb.Append("){");
            foreach (var instructionDefinition in WhileBody.Operations)
            {
                instructionDefinition.WriteCode(sb);
            }
            sb.Append("}"); 
        }
    }
}