using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public static class TokenUtils
    {
        public static Dictionary<int, TokenDef> TokensMatchingInTokenDefs(this IEnumerable<TokenDef> tokenList,
            IEnumerable<TokenKind> tokensToSearch)
        {
            var hashSet = new HashSet<TokenKind>(tokensToSearch);
            var result = new Dictionary<int, TokenDef>();
            var pos = 0;
            foreach (var tokenDef in tokenList)
            {
                if (hashSet.Contains(tokenDef.Kind))
                {
                    result[pos] = tokenDef;
                }
                pos++;
            }
            return result;
        } 
    }
}