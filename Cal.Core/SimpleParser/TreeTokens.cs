using System;
using System.Collections.Generic;
using Cal.Core.Lexer;
using Cal.Core.Utils;

namespace Cal.Core.SimpleParser
{
    public class TreeTokens
    {
        private List<TreeTokens> _items = new List<TreeTokens>();
        private LineTokens _line;

        public LineTokens Line
        {
            get { return _line; }
            set { _line = value; }
        }

        public List<TreeTokens> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public TreeTokens(LineTokens line)
        {
            _line = line;
            FoldByKind();
        }

        private void FoldByKind()
        {
            var index = _line.Items.IndexOfT(child => child.Kind== TokenKind.OpAssign);
            if(index!=-1)
                SplitTree(index);
            FoldRecursely(this);
            }

        private static void FoldRecursely(TreeTokens treeTokens)
        {
            foreach (var treeTokense in treeTokens.Items)
            {
                FoldRecursely(treeTokense);
            }
            FoldFunctionBlocks(treeTokens);

        }

        private static void FoldFunctionBlocks(TreeTokens treeTokens)
        {
            var line = treeTokens.Line.Items;
            for (var i = 0; i < line.Count - 2; i++)
            {
                if (line[i].Kind == TokenKind.Identifier && line[i + 1].Kind == TokenKind.OpOpenParen)
                {
                    var closeParen = GetMatchingCloseParent(line, i + 1);
                    if(closeParen==-1)
                        throw new InvalidOperationException("Script has not matching close paren");
                    var tokensToFold = line.GetRange(i + 2, closeParen - i - 2);

                }
            }

        }

        private static int GetMatchingCloseParent(List<TokenDef> line, int i)
        {
            var openParens = 1;
            for (var index = i + 1; index < line.Count; index++)
            {
                if (line[index].Kind == TokenKind.OpOpenParen)
                    openParens++;
                if (line[index].Kind == TokenKind.OpCloseParen)
                    openParens--;
                if (openParens == 0)
                    return index;
            }
            return -1;
        }


        private void SplitTree(int index)
        {
            if(index==-1)
                return;
            var leftItems = Line.Items.GetRange(0, index);
            var rightItems = Line.Items.GetRange(index + 1, Line.Items.Count - index - 1);
            var rootItem = Line.Items[index];
            var leftData = new TreeTokens
            {
                Line = new LineTokens()
                {
                    Items = leftItems
                }
            };
            Line = new LineTokens(); 
            Line.Items.Add(rootItem);
            var rightData = new TreeTokens
            {
                Line = new LineTokens()
                {
                    Items= rightItems
            }
            };
            _items = new List<TreeTokens>
            {
                leftData, rightData
            };
        }

        public override string ToString()
        {
            return _line.ToString();
        }

        private TreeTokens()
        {
            Line = new LineTokens();
        }
    }
}