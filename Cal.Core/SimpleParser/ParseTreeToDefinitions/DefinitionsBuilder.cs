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
            var tokenDefs = ast.Items;
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
            var firstRow = item.Items[0].RowTokens;
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
            ProcessBodyInstructions(scope, ast, ast.Items.GetRange(1, ast.Items.Count-2).ToArray());
        }

        private void ProcessBodyInstructions(ScopeDefinition scope, AstNode ast, AstNode[] childNodes)
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

        private void ProcesInstructionIf(AstNode item, ScopeDefinition scope)
        {
            var ifBlock = new IfDefinition(item)
            {
                IfBody =
                {
                    ParentScope = scope
                }, 
                ElseBody =
                {
                    ParentScope = scope
                }
            };

            scope.ProcessAddOperation(item, ifBlock);
            var childNodes = item.Items.GetRange(1, item.Items.Count-2).ToArray();
            ProcessBodyInstructions(ifBlock.IfBody, item, childNodes);
        }

        private void ProcesInstructionWhile(AstNode item, ScopeDefinition scope)
        {
            var whileDefinition = new WhileDefinition(item)
            {
                WhileBody = {ParentScope = scope}
            };
            scope.ProcessAddOperation(item, whileDefinition);
            var childNodes = item.Items.GetRange(1, item.Items.Count-2).ToArray();
            ProcessBodyInstructions(whileDefinition.WhileBody, item, childNodes);
        }

        private void ProcessInstructionOrAssign(AstNode item, ScopeDefinition scope)
        {
            bool hasAssign = item.RowTokens.Items.Any(token => token.Kind == TokenKind.OpAssign);
            if (!hasAssign)
                scope.ProcessAddCall(item);
            else
                scope.ProcessAssign(item);
        }
    }
}
