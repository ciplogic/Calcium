using System.Collections.Generic;
using Cal.Core.BlockParser;
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
            _endTokens = new HashSet<TokenKind>
            {
                TokenKind.RwEnd
            };
        }

        HashSet<TokenKind> _startTokens = new HashSet<TokenKind>();
        HashSet<TokenKind> _spacesTokens = new HashSet<TokenKind>();
        private HashSet<TokenKind> _endTokens;
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
                Ast.Items.Add(blockAstNode);
            }
        }

        public readonly List<AstVisitor> TokenVisitors = new List<AstVisitor>();

        public ParseResult Parse()
        {
            BlockFoldings.FoldComments(Ast, _spacesTokens);
            BlockFoldings.FoldNodes(Ast, 0, Ast.Items.Count - 1, _startTokens, _endTokens);

            ParseResult result = new ParseResult();
            result.HasErrors = false;
            result.Ast = Ast;
            return result;
        }

        private void VisitTokens()
        {
            foreach (var visitor in TokenVisitors)
            {
                visitor.Visit(Ast);
            }
        }
    }
}

