using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions;
using Cal.Core.Lexer;

namespace Cal.Core.SimpleParser.ParseTreeToDefinitions
{
    public class DefinitionsBuilder
    {
        public ProgramDefinition Build(ParseResult result)
        {
            if (result.HasErrors) return null;
            ProgramDefinition program=new ProgramDefinition();
            ProcesProgramNode(result.Ast, program);
            return program;
        }

        private void ProcesProgramNode(AstNode ast, ProgramDefinition program)
        {
            var tokenDefs = ast.ChildrenNodes;
            foreach (var item in tokenDefs)
            {
                switch (item.NodeKind)
                {
                    case TokenKind.RwDef:
                        ProcesProgramMethod(item, program);
                        break;
                }
            }
        }

        private void ProcesProgramMethod(AstNode item, ProgramDefinition program)
        {
            var methodDefinition = new MethodDefinition();
            var firstRow = item.ChildrenNodes[0].RowTokens;
            methodDefinition.Name = firstRow.Items[1].GetContent();
            if (firstRow.Count > 2)
            {
                methodDefinition.ProcessArguments(firstRow.Items.GetRange(2, firstRow.Items.Count - 2));
            }
            program.GlobalClass.AddMethodToClass(methodDefinition);
            methodDefinition.IsStatic = true;
            ProcessMethodNode(methodDefinition.MainBody.Scope, item);
        }

        private void ProcessMethodNode(ScopeDefinition scope, AstNode ast)
        {
            ProcessBodyInstructions(scope, ast.ChildrenNodes.GetRange(1, ast.ChildrenNodes.Count-2).ToArray());
        }

        public static void ProcessBodyInstructions(ScopeDefinition scope, AstNode[] childNodes)
        {
            foreach (var item in childNodes)
            {
                var tokenKind = item.NodeKind;
                switch (tokenKind)
                {
                    case TokenKind.RwWhile:
                        ProcesInstructionWhile(item, scope);
                        break;
                    case TokenKind.RwIf:
                        ProcesInstructionIf(item, scope);
                        break;
                    default:
                        ProcessInstructionOrAssign(item, scope);
                        break;
                }
            }
        }

        private static void ProcesInstructionIf(AstNode item, ScopeDefinition scope)
        {
            var ifBlock = new IfDefinition(item,scope);

            scope.ProcessAddOperation(item, ifBlock);
        }

        private static void ProcesInstructionWhile(AstNode item, ScopeDefinition scope)
        {
            var whileDefinition = new WhileDefinition(item)
            {
                WhileBody = {ParentScope = scope}
            };
            scope.ProcessAddOperation(item, whileDefinition);
            var childNodes = item.ChildrenNodes.GetRange(1, item.ChildrenNodes.Count-2).ToArray();
            ProcessBodyInstructions(whileDefinition.WhileBody, childNodes);
        }

        private static void ProcessInstructionOrAssign(AstNode item, ScopeDefinition scope)
        {
            bool hasAssign = item.RowTokens.Items.Any(token => token.Kind == TokenKind.OpAssign);
            if (!hasAssign)
                scope.ProcessAddCall(item);
            else
                scope.ProcessAssign(item);
        }
    }
}
