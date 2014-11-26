using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cal.Core.Definitions;
using Cal.Core.Lexer;

namespace Cal.Core.Semantic
{
    public class SemanticAnalysis
    {
        public void Analyze(ProgramDefinition progDefinition)
        {

        }

        public static void AnalyseFirstAssign(AssignDefinition assign, ScopeDefinition scope)
        {
            MethodDefinition method = scope.Method;
            var arguments = method.Arguments;
            var variables = scope.Variables;
            List<TokenDef> contentTokens = assign.Left.ContentTokens;
            var name = contentTokens[0].GetContent();

            AnalyseExpressionType(assign.RightExpression);

            if (arguments.All(arg => arg.Variable.Name != name) 
                && scope.LocateVariable(name) == null)
            {
                var variableDefinition = new VariableDefinition {Name = name};
                variables.Add(variableDefinition);
                
                variableDefinition.Type = assign.RightExpression.TypeDefinition;
                if (variableDefinition.Type == null)
                {
                    Console.WriteLine(assign);
                }
            }
            
        }

        private static void AnalyseExpressionType(ExpressionDefinition rightExpression)
        {
                    var content = rightExpression.ContentTokens;
            if (content.Count == 1)
            {
                switch (content[0].Kind)
                {
                    case TokenKind.Integer:
                        rightExpression.TypeDefinition = new ClrClassDefinition(typeof(int));
                        break;
                    case TokenKind.Double:
                        rightExpression.TypeDefinition = new ClrClassDefinition(typeof(double));
                        break;
                    case TokenKind.OpSingleQuote:
                        rightExpression.TypeDefinition = new ClrClassDefinition(typeof(string));
                        break;
                }
                return;
            }
            if (content.Any(tok => tok.Kind == TokenKind.Double))
            {
                if (content.All(tok => tok.Kind != TokenKind.Identifier))
                {
                    rightExpression.TypeDefinition = new ClrClassDefinition(typeof (double));
                    return;
                }
            } 
            if (content.Any(tok => tok.Kind == TokenKind.Integer))
            {
                rightExpression.TypeDefinition = new ClrClassDefinition(typeof(int));
                return;
            }
        }
    }
}
