using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;

namespace Cal.Core.ParseTreeToDefinitions
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

            ProcessMethodNode(methodDefinition, methodDefinition.MainBody.Scope, item);
        }

        private void ProcessMethodNode(MethodDefinition methodDefinition, ScopeDefinition scope, AstNode ast)
        {
            ProcessBodyInstructions(methodDefinition, scope, ast);
        }

        private void ProcessBodyInstructions(MethodDefinition methodDefinition, ScopeDefinition scope, AstNode ast)
        {
            var tokenDefs = ast.Items.Skip(1).ToArray();
            foreach (var item in tokenDefs)
            {
                switch (item.NodeKind)
                {
                    case TokenKind.RwWhile:
                        ProcesInstructionWhile(item, methodDefinition, scope);
                        break;
                    case TokenKind.RwIf:
                        ProcesInstructionIf(item, methodDefinition, scope);
                        break;
                    default:
                        ProcessInstructionOrAssign(item, methodDefinition);
                        break;
                }
            }
        }

        private void ProcesInstructionIf(AstNode item, MethodDefinition methodDefinition, ScopeDefinition scope)
        {
            ProcessBodyInstructions(methodDefinition, scope, item);
        }

        private void ProcesInstructionWhile(AstNode item, MethodDefinition methodDefinition, ScopeDefinition scope)
        {
            ProcessBodyInstructions(methodDefinition, scope, item);
        }

        private void ProcessInstructionOrAssign(AstNode item, MethodDefinition methodDefinition)
        {
            bool hasAssign = item.RowTokens.Items.Any(token => token.Kind == TokenKind.OpAssign);
            if (!hasAssign)
                methodDefinition.MainBody.Scope.ProcessAddCall(item);
            else
                methodDefinition.ProcessAssign(item);
        }
    }
}
