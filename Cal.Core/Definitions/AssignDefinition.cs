using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions
{
    public class AssignDefinition : InstructionDefinition
    {
        public AssignDefinition(BlockDefinition blockDefinition, AstNode item, int indexAssign)
            : base(blockDefinition.Scope)
        {
            var tokenDefs = item.RowTokens.Items;
            List<TokenKind> tokenKinds = tokenDefs
              .Select(tok => tok.Kind)
              .ToList();
            var leftTokens = tokenDefs.GetRange(0, indexAssign);
            var rightTOkens = tokenDefs.GetRange(indexAssign + 1, tokenKinds.Count - indexAssign - 1);
            AssignToken = tokenDefs[indexAssign];
            Left = new AssignLeftDefinition(leftTokens, blockDefinition);
            RightExpression = ExpressionResolver.Resolve(rightTOkens, this);
            AddLeftIfOneToken(Left);
        }
        
        public TokenDef AssignToken {get; set;}

        public AssignLeftDefinition Left { get; set; }

        public ExprResolverBase RightExpression { get; set; }


        private void AddLeftIfOneToken(AssignLeftDefinition left)
        {
            if (left.Tokens.Count != 1)
                return;
            var firstToken = left.Tokens[0];
            Scope.AddVariable(firstToken.GetContent());
        }

        public override string ToString()
        {
            return string.Format("{0} {2} {1}", Left, RightExpression, AssignToken.GetContent());
        }

        public override void WriteCode(StringBuilder sb)
        {
            var leftCode = Left.ToCode();
            sb.AppendFormat("{0} = {1};", leftCode, RightExpression.ToCode())
                .AppendLine();
        }
    }
}