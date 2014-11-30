using System;
using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class ExprResolverUnsolved : ExprResolverBase
    {
        
    }
    class ExpressionResolver
    {
        private static TokenKind[] _binaryOperators;

        static ExpressionResolver()
        {

            _binaryOperators = new[]
            {
                TokenKind.OpLessThan,
                TokenKind.OpAdd,
                TokenKind.OpSub
            };
        }
        public static ExprResolverBase Resolve(ExpressionDefinition expressionDefinition)
        {
            var tokens = expressionDefinition.ContentTokens;
            var count = tokens.Count;
            if (count == 1)
            {
                return ResolveIndividual(expressionDefinition, tokens[0]);
            }
            if (count == 2 && tokens[0].Kind!=TokenKind.Identifier)
            {
                return ResolveUnary(expressionDefinition, tokens);
            }
            return HandleMultipleItemsExpression(expressionDefinition);
        }

        private static ExprResolverBase HandleMultipleItemsExpression(ExpressionDefinition expressionDefinition)
        {
            var contentTokens = expressionDefinition.ContentTokens;
            var binaryTokens = GetBinaryTokens(contentTokens);
            var highestPriorityBinaryToken = ComputeHighestPriorityToken(binaryTokens);
            var leftTokens = expressionDefinition.ContentTokens.GetRange(0, highestPriorityBinaryToken.Key);
            var rightTokens = expressionDefinition.ContentTokens.GetRange(highestPriorityBinaryToken.Key + 1, expressionDefinition.ContentTokens.Count - highestPriorityBinaryToken.Key - 1);
            var binaryExpression = new ExprBinaryResolved(highestPriorityBinaryToken.Value, leftTokens,rightTokens,expressionDefinition);
            return binaryExpression;
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


        private static ExprResolverBase ResolveUnary(ExpressionDefinition expressionDefinition, List<TokenDef> tokens)
        {
            ExprResolverBase result = new ExprUnaryResolved(tokens[0], tokens.GetRange(1, tokens.Count - 1), expressionDefinition);

            return result;
        }

        private static ExprResolverBase ResolveIndividual(ExpressionDefinition expressionDefinition, TokenDef contentToken)
        {
            switch (contentToken.Kind)
            {
                case TokenKind.Double:
                    return new ValueExpressionDouble(contentToken);
                case TokenKind.Integer:
                    return new ValueExpressionDouble(contentToken);
                case TokenKind.Identifier:
                    return ResolveVariableOrFunction(expressionDefinition, contentToken);


            }
            
            throw new NotImplementedException();
        }

        private static ExprResolverBase ResolveVariableOrFunction(ExpressionDefinition expressionDefinition, TokenDef contentToken)
        {
            var parentScope = expressionDefinition.ParentDefinition.Scope;
            var content = contentToken.GetContent();
            var variable = parentScope.LocateVariable(content);
            if (variable == null)
            {
                throw new NotImplementedException();
            }
            ExprResolverBase result = new VariableResolved(variable);
            return result;
        }
    }
}
