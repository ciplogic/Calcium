﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Definitions.Instruction;
using Cal.Core.Definitions.ReferenceDefinitions;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.Definitions.Assigns
{
    public class AssignDefinition : InstructionDefinition
    {
        public AssignDefinition(BlockDefinition blockDefinition, AstNode item, int indexAssign)
            : base(blockDefinition)
        {
            var tokenDefs = item.RowTokens.Items;
            List<TokenKind> tokenKinds = tokenDefs
              .Select(tok => tok.Kind)
              .ToList();
            var leftTokens = tokenDefs.GetRange(0, indexAssign);
            var rightTOkens = tokenDefs.GetRange(indexAssign + 1, tokenKinds.Count - indexAssign - 1);
            AssignToken = tokenDefs[indexAssign];
            Left = new AssignLeftDefinition(leftTokens, blockDefinition);
            RightExpression = ExpressionResolver.Resolve(rightTOkens, blockDefinition);
            AssignTypeForVariable();
        }

        private void AssignTypeForVariable()
        {
            if (!RightExpression.CalculateExpressionType())
                return;
            var refVarDefinition = Left.ReferenceDefinition as ReferenceVariableDefinition;
            if (refVarDefinition != null && refVarDefinition.VariableDefinition.Type==null)
            {
                refVarDefinition.VariableDefinition.Type = RightExpression.ExpressionType;
            }
        }

        public TokenDef AssignToken {get; set;}

        public AssignLeftDefinition Left { get; set; }

        public ExprResolverBase RightExpression { get; set; }


        public override string ToString()
        {
            return string.Format("{0} {2} {1}", Left, RightExpression, AssignToken.GetContent());
        }

        public override void WriteCode(StringBuilder sb)
        {
            var leftCode = Left.ToCode();
            sb.AppendFormat("{0} {2} {1};", leftCode, RightExpression.ToCode(), AssignToken.GetContent())
                .AppendLine();
        }
    }
}