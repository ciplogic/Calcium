using System.CodeDom.Compiler;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class WhileDefinition : InstructionDefinition
    {
        public ExpressionDefinition WhileExpression { get; set; }
        public ScopeDefinition WhileBody { get; set; }

        public WhileDefinition(AstNode item)
        {
            WhileBody = new ScopeDefinition();
            WhileExpression = new ExpressionDefinition(item.Items[0].RowTokens.Items.Skip(1).ToList())
            {
                ParentDefinition = this
            };
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append("while(");
            sb.Append(WhileExpression);
            sb.Append("){");
            foreach (var instructionDefinition in WhileBody.Operations)
            {
                instructionDefinition.WriteCode(sb);
            }
            sb.Append("}"); 
        }
    }
}