using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions;
using Cal.Core.Lexer;

namespace Cal.Core.SimpleParser.ParseTreeToDefinitions
{
    public class DefinitionsBuilder
    {
        public List<BaseDefinition> Definitions = new List<BaseDefinition>(); 
        public ProgramDefinition Build(ParseResult result)
        {
            if (result.HasErrors) return null;
            ProgramDefinition program=new ProgramDefinition();
            Definitions.Add(program);
            ProcesProgramNode(result.Ast, program, Definitions);
            return program;
        }

        private void ProcesProgramNode(AstNode ast, ProgramDefinition program, List<BaseDefinition> definitions)
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

            ProcessMethodNode(methodDefinition.MainBody.Scope, item);
        }

        private void ProcessMethodNode(ScopeDefinition scope, AstNode ast)
        {
            ProcessBodyInstructions(scope, ast);
        }

        private void ProcessBodyInstructions(ScopeDefinition scope, AstNode ast)
        {
            var tokenDefs = ast.Items.GetRange(1, ast.Items.Count-2).ToArray();
            foreach (var item in tokenDefs)
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
            var ifBlock = new IfDefinition(item);
            scope.ProcessAddOperation(item, ifBlock);
            ProcessBodyInstructions(ifBlock.IfBody, item);
        }

        private void ProcesInstructionWhile(AstNode item, ScopeDefinition scope)
        {
            var whileDefinition = new WhileDefinition(item);
            scope.ProcessAddOperation(item, whileDefinition);
            ProcessBodyInstructions(whileDefinition.WhileBody, item);
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
