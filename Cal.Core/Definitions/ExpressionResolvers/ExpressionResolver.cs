using System;
using System.Collections.Generic;
using Cal.Core.Definitions.ExpressionResolvers.Nodes;
using Cal.Core.Definitions.IdentifierDefinition;
using Cal.Core.Definitions.ReferenceDefinitions;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    class ExpressionResolver
    {
        private static TokenKind[] _binaryOperators;

        static ExpressionResolver()
        {

            _binaryOperators = new[]
            {
                TokenKind.OpEquals,
                TokenKind.OpGreaterThan,
                TokenKind.OpGreaterStrict,
                TokenKind.OpLessThan,
                TokenKind.OpLessStrict,
                TokenKind.OpMul,
                TokenKind.OpDiv,
                TokenKind.OpAdd,
                TokenKind.OpSub
            };
        }
        public static ExprResolverBase Resolve(List<TokenDef> tokens, BlockDefinition instructionDefinition)
        {
            var count = tokens.Count;
            if (count == 1)
            {
                return ResolveIndividual(instructionDefinition, tokens[0]);
            }
            if (count == 2 && tokens[0].Kind != TokenKind.Identifier)
            {
                return ResolveUnary(instructionDefinition, tokens);
            }
            return HandleMultipleItemsExpression(instructionDefinition, tokens);
        }
        private static ExprResolverBase HandleMultipleItemsExpression(BlockDefinition instructionDefinition, List<TokenDef> tokens)
        {
            var contentTokens = tokens;
            if (IsFunction(contentTokens))
            {
                return FunctionResolve(contentTokens, instructionDefinition);
            }
            if (IsParen(contentTokens))
            {
                return ParenResolve(contentTokens, instructionDefinition);
            }
            var binaryTokens = GetBinaryTokens(contentTokens);
            var highestPriorityBinaryToken = ComputeHighestPriorityToken(binaryTokens);
            var leftTokens = tokens.GetRange(0, highestPriorityBinaryToken.Key);
            var rightTokens = tokens.GetRange(highestPriorityBinaryToken.Key + 1, tokens.Count - highestPriorityBinaryToken.Key - 1);
            var binaryExpression = new BinaryExpression(highestPriorityBinaryToken.Value, leftTokens,rightTokens,instructionDefinition);
            return binaryExpression;
        }

        private static ExprResolverBase ParenResolve(List<TokenDef> contentTokens, BlockDefinition instructionDefinition)
        {
            var resolvedParen = new ParenResolved(contentTokens.GetRange(1, contentTokens.Count-2), instructionDefinition);
            return resolvedParen;
        }

        private static bool IsParen(List<TokenDef> contentTokens)
        {
            if (contentTokens.Count <= 2)
                return false;
            if (contentTokens[0].Kind != TokenKind.OpOpenParen)
                return false;
            if (contentTokens[contentTokens.Count-1].Kind != TokenKind.OpCloseParen)
                return false;
            return true;
        }

        private static ExprResolverBase FunctionResolve(List<TokenDef> contentTokens, BlockDefinition instructionDefinition)
        {
            ExprResolverBase result = new FunctionCallResolved(contentTokens, instructionDefinition);
            return result;
        }

        private static bool IsFunction(List<TokenDef> contentTokens)
        {
            if (contentTokens[0].Kind == TokenKind.Identifier &&
                contentTokens[1].Kind == TokenKind.OpOpenParen)
                return true;
            return false;
        }

        private static KeyValuePair<int, TokenDef> ComputeHighestPriorityToken(Dictionary<int, TokenDef> binaryTokens)
        {
            if (binaryTokens.Count == 0)
            {
                throw new InvalidOperationException("Should have at least one item");
            }
            var firstOfKinds = FirstOfKinds(binaryTokens, _binaryOperators);
            if (!firstOfKinds.HasValue)
            {
                throw new InvalidOperationException("Unknown operator kind");
            }
            return firstOfKinds.Value;
        }

        static KeyValuePair<int, TokenDef>? FirstOfKinds(Dictionary<int, TokenDef> binaryTokens, IEnumerable<TokenKind> kinds)
        {
            foreach (var tokenKind in kinds)
            {
                var itemResult = FirstOfKind(binaryTokens, tokenKind);
                if (itemResult != null)
                    return itemResult;
            }
            return null;
        }

        static KeyValuePair<int, TokenDef>? FirstOfKind(Dictionary<int, TokenDef> binaryTokens, TokenKind kind)
        {
            foreach (var binaryToken in binaryTokens)
            {
                if (binaryToken.Value.Kind == kind)
                    return binaryToken;
            }
            return null;
        }

        private static Dictionary<int, TokenDef> GetBinaryTokens(List<TokenDef> contentTokens)
        {
            return contentTokens.TokensMatchingInTokenDefs(_binaryOperators);
        }


        private static ExprResolverBase ResolveUnary(BlockDefinition expressionDefinition, List<TokenDef> tokens)
        {
            ExprResolverBase result = new ExprUnaryResolved(tokens[0], tokens.GetRange(1, tokens.Count - 1), expressionDefinition);

            return result;
        }

        private static ExprResolverBase ResolveIndividual(BlockDefinition instructionDefinition, TokenDef contentToken)
        {
            switch (contentToken.Kind)
            {
                case TokenKind.Double:
                    return new ValueExpressionT<double>(contentToken, double.Parse);
                case TokenKind.Integer:
                    return new ValueExpressionT<int>(contentToken, int.Parse);
                case TokenKind.Boolean:
                    return new ValueExpressionT<bool>(contentToken, bool.Parse);
                case TokenKind.OpSingleQuote:
                    return new ValueExpressionT<string>(contentToken, s=>s);
                case TokenKind.Identifier:
                    return ResolveVariableOrFunction(instructionDefinition, contentToken);
            }
            
            throw new NotImplementedException();
        }

        private static ExprResolverBase ResolveVariableOrFunction(BlockDefinition expressionDefinition, TokenDef contentToken)
        {
            var parentBlock = expressionDefinition;
            var variableRef = (ReferenceVariableDefinition)parentBlock.LocateVariable(contentToken);
            if (variableRef== null||variableRef.VariableDefinition==null)
            {
                return null;
            }
            var result = new VariableResolved(variableRef.VariableDefinition);
            return result;
        }

        public static ExprResolverBase ResolveMethod(List<TokenDef> tokens, BlockDefinition instructionDefinition)
        {  
            var firstToken = tokens[0].Kind;
              
            if (tokens.Count == 1)
            {
                switch (firstToken)
                {
                    case TokenKind.RwBreak:
                        return new BreakExpression();
                    case TokenKind.Identifier:
                        var result = Resolve(tokens, instructionDefinition);
                        if (result != null)
                            return result;
                        break;
                }
            }

            return ResolveMethodInRuntime(tokens);
        }

        private static ExprResolverBase ResolveMethodInRuntime(List<TokenDef> tokens)
        {
            return ReferenceResolver.Instance.ResolveMethod(tokens[0].GetContent(), tokens.Count-1);
        }
    }
}
