using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;

namespace Cal.Core.BlockParser
{
    public class LineTokens
    {
        public List<TokenDef> Items = new List<TokenDef>();
        public int Count { get { return Items.Count; }}

        public override string ToString()
        {
            if (Items.Count == 1)
                return Items[0].ToString();
            var textItems = Items.Select(it => it.GetContent()).ToArray();
            return string.Join(" ", textItems);
        }

        public void Add(TokenDef tokenDef)
        {
            Items.Add(tokenDef);
        }

        static readonly HashSet<TokenKind> SpacesTokens = new HashSet<TokenKind>
            {
	            TokenKind.Spaces,
	            TokenKind.Comment,
	            TokenKind.Eoln,
	        };
        public void CleanSpaces()
        {
            var items = Items;
            var toRemove = new List<int>();
            for (int index = 0; index < items.Count; index++)
            {
                var item = items[index];
                if (SpacesTokens.Contains(item.Kind))
                    toRemove.Add(index);
            }
            toRemove.Reverse();
            foreach (var index in toRemove)
            {
                items.RemoveAt(index);
            }
        }
    }
}