using System.Collections.Generic;
using System.Linq;
using Cal.Core.Definitions.Assigns;
using Cal.Core.Definitions.ExpressionResolvers;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser;
using Cal.Core.Utils;

namespace Cal.Core.Definitions
{
    public class DefinitionsBuilder
    {
        public ProgramDefinition Build(ParseResult result)
        {
            if (result.HasErrors) return null;
            var program = ProgramDefinition.Instance;
            ProcesProgramNode(result.Ast, program);
            return program;
        }

        static TokenKind GetFoldNodeKind(AstNode ast)
        {
            return ast.NodeKind == TokenKind.FoldedNode
                ? ast.ChildrenNodes[0].RowTokens.Items[0].Kind
                : TokenKind.None;
        }

        private void ProcesProgramNode(AstNode ast, ProgramDefinition program)
        {
            var tokenDefs = ast.ChildrenNodes;
            foreach (var item in tokenDefs)
            {
                var foldNodeKind = GetFoldNodeKind(item);
                switch (foldNodeKind)
                {
                    case TokenKind.RwDef:
                        ProcesProgramMethod(item, program);
                        break;
                    default:
                        var mainMethod = program.GlobalClass.Defs.First(def => def.Name == "Main");
                        ProcessInstructionInBlock(mainMethod, item);
                        break;
                }
            }
        }

        private void ProcesProgramMethod(AstNode item, ProgramDefinition program)
        {
            var globalClass = ProgramDefinition.Instance.GlobalClass;
            var methodDefinition = new MethodDefinition(globalClass);
            var firstRow = item.ChildrenNodes[1].RowTokens;
            methodDefinition.ProcessMethodHeader(firstRow);
            methodDefinition.IsStatic = true;
            globalClass.AddMethodToClass(methodDefinition);
            ProcessBodyInstructions(methodDefinition, item.ChildrenNodes[2].ChildrenNodes.ToArray());
        }


        public static BlockDefinition BuildScopeFromOperations(BlockDefinition parentScope, string titleScope, AstNode[] rangeNodes, BlockKind kind)
        {
            var buildScope = new BlockDefinition(parentScope, titleScope, kind);
            var elseBranchOps = rangeNodes;
            ProcessBodyInstructions(buildScope, elseBranchOps);
            return buildScope;
        }

        public static void ProcessBodyInstructions(BlockDefinition scope, AstNode[] childNodes)
        {
            foreach (var item in childNodes)
            {
                ProcessInstructionInBlock(scope, item);
            }
        }

        private static void ProcessInstructionInBlock(BlockDefinition scope, AstNode item)
        {
            var tokenKind = GetFoldNodeKind(item);
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

        private static void ProcesInstructionIf(AstNode item, BlockDefinition scope)
        {
            var ifBlock = new IfDefinition(item, scope);

            scope.Scope.ProcessAddOperation(ifBlock);
        }

        private static void ProcesInstructionWhile(AstNode item, BlockDefinition scope)
        {
            var whileDefinition = new WhileDefinition(item, scope);
            scope.Scope.ProcessAddOperation(whileDefinition);
            AstNode[] childNodes = item.ChildrenNodes[2].ChildrenNodes.ToArray();
            ProcessBodyInstructions(whileDefinition.WhileBody, childNodes);
        }

        static readonly HashSet<TokenKind> AssignTokenKinds = new HashSet<TokenKind>{
        	TokenKind.OpAddBy,
        	TokenKind.OpAssign
        };

        static bool IsAssignOperator(TokenDef token)
        {
            return AssignTokenKinds.Contains(token.Kind);
        }

        private static void ProcessInstructionOrAssign(AstNode item, BlockDefinition blockDefinition)
        {
            var tokenKinds = item.RowTokens.Items;
            bool hasAssign = tokenKinds.Any(IsAssignOperator);
            if (!hasAssign)
            {
                ExprResolverBase instructionResolver = ExpressionResolver.ResolveMethod(tokenKinds, blockDefinition);
                if (instructionResolver.Kind == ExpressionKind.FunctionCall)
                    blockDefinition.Scope.ProcessAddCall(item, blockDefinition, instructionResolver);
                else
                {
                    blockDefinition.Scope.AddResolvedOperation(instructionResolver, blockDefinition);
                }
            }
            else
            {
                var indexAssignOp = tokenKinds
                    .IndexOfT(IsAssignOperator);
                var assign = new AssignDefinition(blockDefinition, item, indexAssignOp);
                blockDefinition.Scope.Operations.Add(assign);
            }
        }
    }
}
