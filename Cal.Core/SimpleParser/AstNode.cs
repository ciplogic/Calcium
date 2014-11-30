using System.Collections.Generic;
using System.Linq;
using Cal.Core.Lexer;

namespace Cal.Core.SimpleParser
{
    public class AstNode {
        private readonly List<AstNode> _childrenNodes = new List<AstNode>();

		public LineTokens RowTokens { get; set; }
        public TreeTokens Tree { get; set; }

        public TokenKind NodeKind { get; set; }

        public List<AstNode> ChildrenNodes
        {
            get { return _childrenNodes; }
        }

        public AstNode()
        {
            NodeKind = TokenKind.NonTerminal;
            RowTokens = new LineTokens();
		}

        public AstNode(LineTokens tokens)
		{
			RowTokens = tokens;
            NodeKind = TokenKind.Terminal;
            Tree = new TreeTokens(tokens);
		}

		public AstNode BuildNonTerminal(int startRange, int endRange, TokenKind parseKind)
		{
			var result = new AstNode ();
			for (var i = startRange; i <= endRange; i++)
			{
			    var node = _childrenNodes[i];
                result._childrenNodes.Add(node);
			}
			_childrenNodes.RemoveRange(startRange, endRange- startRange+1);
			_childrenNodes.Insert (startRange, result);
		    result.NodeKind = parseKind;
			return result;
		}

        public List<AstNode> SubRange(int skipStart, int skipEnd)
        {
            return _childrenNodes.GetRange(skipStart, _childrenNodes.Count - skipStart - skipEnd).ToList();
        }

        public List<AstNode> Range(int skipStart, int count)
        {
            return _childrenNodes.GetRange(skipStart, count).ToList();
        }
        public List<AstNode> SubRange(int skipStart)
        {
            return _childrenNodes.GetRange(skipStart, _childrenNodes.Count - skipStart).ToList();
        } 

	    public override string ToString()
	    {
	        return string.Format("{0} ({1})", RowTokens,NodeKind);
	    }
	}
	
}
