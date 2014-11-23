using System.Collections.Generic;
using Cal.Core.BlockParser;
using Cal.Core.Lexer;

namespace Cal.Core.SimpleParser
{
    public class AstNode {
		public List<AstNode> Items = new List<AstNode>();

		public LineTokens RowTokens { get; set; }
        public TokenKind NodeKind { get; set; }
		public AstNode()
        {
            NodeKind = TokenKind.NonTerminal;
            RowTokens = new LineTokens();
		}

        public AstNode(LineTokens tokens)
		{
			RowTokens = tokens;
            NodeKind = TokenKind.Terminal;
		}

		public AstNode BuildNonTerminal(int startRange, int endRange, TokenKind parseKind)
		{
			var result = new AstNode ();
			for (var i = startRange; i <= endRange; i++)
			{
			    var node = Items[i];
                result.Items.Add(node);
			}
			Items.RemoveRange(startRange, endRange- startRange+1);
			Items.Insert (startRange, result);
		    result.NodeKind = parseKind;
			return result;
		}

	    public override string ToString()
	    {
	        return string.Format("{0} ({1})", RowTokens,NodeKind);
	    }
	}
	
}
