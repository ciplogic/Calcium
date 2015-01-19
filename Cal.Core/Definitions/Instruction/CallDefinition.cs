using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Definitions.ExpressionResolvers.Nodes;
using Cal.Core.Definitions.Instruction;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions
{
    public class CallDefinition : InstructionDefinition
    {
        private readonly ExprResolverBase _instructionResolver;
        public List<TokenDef> CallingClass { get; set; }
        public string MethodName { get; set; }
        public List<ExprResolverBase> Arguments { get; set; }

        public CallDefinition(List<TokenDef> tokenDefs, BlockDefinition scope, ExprResolverBase instructionResolver) : base(scope)
        {
            _instructionResolver = instructionResolver;
            CallingClass = new List<TokenDef>();
            Arguments = new List<ExprResolverBase>();
            var hasParen = tokenDefs.Any(tok => tok.Kind == TokenKind.OpOpenParen);
            if (!hasParen)
            {
                EvaluateWithoutParen(tokenDefs);
            }
        }

        private void EvaluateWithoutParen(List<TokenDef> tokenDefs)
        {
            MethodName = tokenDefs[0].GetContent();
            for (int i = 1; i < tokenDefs.Count; i++)
            {
                var tok = tokenDefs[i];
                var tokenAsList = new List<TokenDef>{tok};
                Arguments.Add(ExpressionResolver.Resolve(tokenAsList, ParentBlock));
            }
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            WriteCode(sb);
            return sb.ToString();
        }

        public override void WriteCode(StringBuilder sb)
        {
            sb.Append(_instructionResolver.ToCode());
            //sb.AppendFormat("({0});",
              //  string.Join(", ", Arguments.Select(arg => arg.ToCode())));
            sb.AppendLine();
        }
    }
}