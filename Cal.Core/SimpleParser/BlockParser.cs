using System.Collections.Generic;
using Cal.Core.Lexer;
using Cal.Core.SimpleParser.Strategies;

namespace Cal.Core.SimpleParser
{
    public class BlockParser
    {
        private void SetupStartBlockTokens()
        {
            _startTokens = new HashSet<TokenKind>
            {
	            TokenKind.RwDef,
	            TokenKind.RwClass,
	            TokenKind.RwIf,
	            TokenKind.RwWhile,
	            TokenKind.RwModule, 
                TokenKind.RwDo
	        };
            _spacesTokens = new HashSet<TokenKind>
            {
	            TokenKind.Spaces,
	            TokenKind.Comment,
	            TokenKind.Eoln,
	        };
        }

        HashSet<TokenKind> _startTokens = new HashSet<TokenKind>();
        HashSet<TokenKind> _spacesTokens = new HashSet<TokenKind>();
        TokenKind[] _startParen = {TokenKind.OpOpenParen};
        public AstNode Ast { get; set; }


        public BlockParser(List<TokenDef> tokens)
        {
            var multiLines = new MultiLinesTokens(tokens);
            SetupStartBlockTokens();

            BuildInitialTree(multiLines.Lines);
        }


        private void BuildInitialTree(List<LineTokens> tokens)
        {
            Ast = new AstNode();

            foreach (var token in tokens)
            {
                var blockAstNode = new AstNode(token);
                Ast.ChildrenNodes.Add(blockAstNode);
            }
        }

        public ParseResult Parse()
        {
            BlockFoldings.FoldComments(Ast, _spacesTokens);
            BlockFoldings.FoldNodes(Ast, 0, Ast.ChildrenNodes.Count - 1, _startTokens, TokenKind.RwEnd);

            var result = new ParseResult
            {
                HasErrors = false, 
                Ast = Ast
            };
            return result;
        }
    }
}

