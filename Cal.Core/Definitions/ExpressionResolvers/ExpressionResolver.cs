using System;
using System.Linq;
using System.Text;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public class ExprResolverUnsolved : ExprResolverBase
    {
        
    }
    class ExpressionResolver
    {


        public static ExprResolverBase Resolve(ExpressionDefinition expressionDefinition)
        {
            if (expressionDefinition.ContentTokens.Count == 1)
            {
                return ResolveIndividual(expressionDefinition, expressionDefinition.ContentTokens[0]);
            }

            throw new NotImplementedException();
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
