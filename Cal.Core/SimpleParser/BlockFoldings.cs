using System;
using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;

namespace Cal.Core.SimpleParser
{
    static class BlockFoldings
    {
        public static string TokenJoinContent(this List<TokenDef> tokenDefs)
        {
            return String.Join(" ", tokenDefs.Select(tok => tok.GetContent()));
        }

        public static List<List<TokenDef>> SplitBlockByToken(this List<TokenDef> allTokens, TokenKind tokenKind)
        {
            List<List<TokenDef>> result = new List<List<TokenDef>>();
            if (allTokens.All(tok => tok.Kind != tokenKind))
            {
                result.Add(allTokens);
                return result;
            }
            List<TokenDef> currentRow = new List<TokenDef>();
            result.Add(currentRow);
            for(int pos = 0; pos<allTokens.Count;pos++)
            {
                var item = allTokens[pos];
                if (item.Kind != tokenKind)
                {
                    currentRow.Add(item);
                }
                else
                {
                    currentRow = new List<TokenDef>();
                    result.Add(currentRow);
                }
            }
            return result;
        }

        public static int GetFirstIndexInRange(List<AstNode> items, int start, int endRange, TokenKind tokenKind)
        {
            var foundIndex = -1;
            if (endRange >= items.Count)
                return -1;
            for (var i = start; i <= endRange; i++)
            {
                var tokenList = items[i].RowTokens.Items;
                if (tokenList.Any(it => it.Kind == tokenKind))
                {
                    foundIndex = i;
                    break;
                }
            }
            return foundIndex;
        }
        
        public static int GetFirstTokenIndexInRange(List<TokenDef> items, int start, int endRange, TokenKind tokenKind)
        {
            var foundIndex = -1;
            if (endRange >= items.Count)
                return -1;
            for (var i = start; i <= endRange; i++)
            {
                var item = items[i].Kind;
                if (item == tokenKind)
                {
                    foundIndex = i;
                    break;
                }
            }
            return foundIndex;
        }

        public static int GetLastIndexInRange(List<AstNode> items, int start, int endRange, HashSet<TokenKind> tokenKind)
        {
            var foundIndex = -1;
            for (var i = endRange; i >= start; i--)
            {
                var tokenList = items[i].RowTokens.Items;
                if (tokenList.Any(it => tokenKind.Contains(it.Kind)))
                {
                    foundIndex = i;
                    break;
                }
            }
            return foundIndex;
        }


        public static void FoldNodes(AstNode ast, int start, int endRange, IEnumerable<TokenKind> startingTokens, TokenKind endTokenKind)
        {
            HashSet<TokenKind> startTokens = new HashSet<TokenKind>(startingTokens);
            var items = ast.ChildrenNodes;

            int foundIndex;
            int getLastInRange;
            do
            {
                foundIndex = GetFirstIndexInRange(items, start, endRange, endTokenKind);
                getLastInRange = -1;
                if (foundIndex != -1)
                {
                    getLastInRange = GetLastIndexInRange(items, start, foundIndex, startTokens);
                    if (getLastInRange != -1)
                    {
                        var foldedNode = ast.BuildNonTerminal(getLastInRange, foundIndex, TokenKind.NonTerminal);
                        var fNodeItems = foldedNode.ChildrenNodes;
                        if (fNodeItems.Count > 0)
                        {
                            foldedNode.NodeKind = fNodeItems.First().RowTokens.Items.First().Kind;
                        }
                        endRange = items.Count - 1;
                    }
                }

            } while (foundIndex != -1 && getLastInRange != -1);

        }

        public static void FoldComments(AstNode ast, HashSet<TokenKind> spacesTokens)
        {
            var items = ast.ChildrenNodes;
            while (true)
            {
                var endRange = items.Count - 1;
                var foundIndex = GetFirstIndexInRange(items, 0, endRange, TokenKind.Comment);
                if (foundIndex == -1)
                    return;

                for (var i = foundIndex + 1; i <= endRange; i++)
                {
                    var tokenList = items[i].RowTokens.Items;
                    if (tokenList.Any(it => spacesTokens.Contains(it.Kind)))
                    {
                        ast.BuildNonTerminal(foundIndex, i - 1, TokenKind.NonTerminal);
                        break;
                    }
                }
            }
        }
    }
}
