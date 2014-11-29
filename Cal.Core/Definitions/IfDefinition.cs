using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;
using Cal.Core.SimpleParser.ParseTreeToDefinitions;
using Cal.Core.Utils;

namespace Cal.Core.Definitions
{
    public class IfDefinition : InstructionDefinition
    {
        public ExpressionDefinition IfExpression { get; set; }
        public ScopeDefinition IfBody { get; set; }
        public ScopeDefinition ElseBody { get; set; }

        public IfDefinition(AstNode item, ScopeDefinition parentScope)
        {
            IfExpression = new ExpressionDefinition(item.ChildrenNodes[0].RowTokens.Range(1));
            IfBody = new ScopeDefinition()
            {
                ParentScope = parentScope
            };
            ElseBody = new ScopeDefinition()
            {
                ParentScope = parentScope
            };
            
            Process(item);
        }

		void Process(AstNode astNode)
		{
			var containsElse = astNode.ChildrenNodes.IndexOfT(IsElseToken);
		    if (containsElse == -1)
		    {
		        ProcessIfBody(IfBody, astNode.SubRange(1,1).ToList());
		    }
		    else
		    {
                ProcessIfBody(IfBody, astNode.Range(1, containsElse- 1).ToList());
                ProcessIfBody(ElseBody, astNode.SubRange(containsElse+1, 1).ToList());
		        
		    }
		}

        static bool IsElseToken(AstNode candidateElse)
        {
            if (candidateElse.NodeKind != TokenKind.Terminal)
                return false;
            return candidateElse.RowTokens.Items[0].Kind == TokenKind.RwElse;
        }

        private void ProcessIfBody(ScopeDefinition codeBlock, List<AstNode> getRange)
        {
            DefinitionsBuilder.ProcessBodyInstructions(codeBlock, getRange.ToArray());
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append("if(");
            IfExpression.WriteCode(sb);
            sb.Append(")");
            IfBody.WriteCode(sb);
           
            if (ElseBody.Operations.Count > 0)
            {
                sb.AppendLine("else");
                ElseBody.WriteCode(sb);
            }
        }
    }
}