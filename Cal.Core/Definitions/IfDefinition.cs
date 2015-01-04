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
        public BlockDefinition IfBody { get; set; }
        public BlockDefinition ElseBody { get; set; }

        public IfDefinition(AstNode item, BlockDefinition parentScope) : base(parentScope)
        {
            IfExpression = ExpressionResolver.Resolve(item.ChildrenNodes[1].RowTokens.Items, parentScope);
            Process(item, parentScope);
        }

		void Process(AstNode astNode, BlockDefinition parentScope)
		{
            astNode = astNode.ChildrenNodes[2];
			var containsElse = astNode.ChildrenNodes.IndexOfT(IsElseToken);
		    if (containsElse == -1)
		    {
                IfBody = DefinitionsBuilder.BuildScopeFromOperations(parentScope, "IfBody", 
                    astNode.ChildrenNodes.ToArray(), 
                    BlockKind.Instruction);
		    }
		    else
		    {
                IfBody = DefinitionsBuilder.BuildScopeFromOperations(parentScope, "IfBody", 
                    astNode.Range(0, containsElse).ToArray(), BlockKind.Instruction);
                ElseBody = DefinitionsBuilder.BuildScopeFromOperations(parentScope, "ElseBody", 
                    astNode.SubRange(containsElse + 1, 0).ToArray(), BlockKind.Instruction); 
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
            IfBody.Scope.WriteCode(sb);
           
            if (ElseBody!=null)
            {
                sb.AppendLine("else");
                ElseBody.Scope.WriteCode(sb);
            }
        }
    }
}