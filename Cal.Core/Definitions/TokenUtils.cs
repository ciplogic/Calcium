using System.Collections.Generic;
using Cal.Core.Lexer;

namespace Cal.Core.Definitions.ExpressionResolvers
{
    public static class TokenUtils
    {
    	public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
    	{
    		var result = new HashSet<T>(items);
    		return result;
    	}
        public static Dictionary<int, TokenDef> TokensMatchingInTokenDefs(this IEnumerable<TokenDef> tokenList,
            HashSet<TokenKind> tokensToSearch)
        {
            var hashSet = tokensToSearch;
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