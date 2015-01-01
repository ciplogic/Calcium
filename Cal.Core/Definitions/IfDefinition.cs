using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;
using Cal.Core.Utils;

namespace Cal.Core.Definitions
{
    public class IfDefinition : InstructionDefinition
    {
        public ExprResolverBase IfExpression { get; private set; }
        public ScopeDefinition IfBody { get; set; }
        public ScopeDefinition ElseBody { get; set; }

        public IfDefinition(AstNode item, ScopeDefinition parentScope) : base(parentScope)
        {
            Process(item, parentScope);
        }

		void Process(AstNode astNode, ScopeDefinition parentScope)
		{
            astNode = astNode.ChildrenNodes[2];
			var containsElse = astNode.ChildrenNodes.IndexOfT(IsElseToken);
		    if (containsElse == -1)
		    {
                IfBody = DefinitionsBuilder.BuildScopeFromOperations(parentScope, "IfBody", astNode.ChildrenNodes.ToArray());
		    }
		    else
		    {
                IfBody = DefinitionsBuilder.BuildScopeFromOperations(parentScope, "IfBody", astNode.Range(0, containsElse).ToArray());
                ElseBody = DefinitionsBuilder.BuildScopeFromOperations(parentScope, "ElseBody", astNode.SubRange(containsElse + 1, 0).ToArray()); 
		    }
		}

        static bool IsElseToken(AstNode candidateElse)
        {
            if (candidateElse.NodeKind != TokenKind.Terminal)
                return false;
            return candidateElse.RowTokens.Items[0].Kind == TokenKind.RwElse;
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append("if(");
            sb.AppendFormat(IfExpression.ToCode());
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