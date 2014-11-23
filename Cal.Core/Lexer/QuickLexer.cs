using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cal.Core.Lexer.Matchers;
using Cal.Core.NewLexer;
using Cal.Core.NewLexer.Matchers;

namespace Cal.Core.Lexer
{
    public class QuickLexer
    {
        public List<TokenDef> Scan(string fileName)
        {
            var allLines = File.ReadAllLines(fileName);

            return Tokenize(allLines);
        }

        private static List<TokenDef> Tokenize(string[] allLines)
        {
            var tokens = new List<TokenDef>();
            
            var row = 0;
            var col = 0;
            tokens.Clear();
            while (true)
            {
                var foundMatch = false;
                if (row == allLines.Length)
                {
                    tokens.Add(new TokenDef(TokenKind.Eof, row, col));
                    break;
                }
                var line = allLines[row];

                if (col == line.Length)
                {
                    tokens.Add(new TokenDef(TokenKind.Eoln, row, col));
                    col = 0;
                    row++;

                    continue;
                }

                var remainingLine = line.Substring(col);
                foreach (var rule in IteratingRules)
                {
                    var matchResult = rule.GetMatchResult(remainingLine);
                    if (matchResult.Kind == TokenKind.None) continue;
                    foundMatch = true;
                    var token = new TokenDef(line, matchResult.Kind, row, col, matchResult.Length);
                    col += matchResult.Length;
                    tokens.Add(token);
                    break;
                }
                if (!foundMatch)
                {
                    throw new InvalidDataException(
                        string.Format("Text cannot be parsed:[{0}]", remainingLine));
                }
            }
            return tokens;
        }

        static readonly LexerMatcher[] IteratingRules;

        static QuickLexer()
        {
            var rules = new List<LexerMatcher>()
            {
                new CommentsMatcher(),
                new DoubleQuoteMatcher(),
                new EnvironmentIdentifierMatcher(),
                new MemberIdentifierMatcher(),
                new NumberMatcher(),
                new OperatorsLexerMatcher(),
                new ReservedWordsIdentifierMatcher(),
                new SingleQuoteMatcher(),
                new SpacesMatcher()
            };

            IteratingRules = rules.ToArray();
        }

    }
}

