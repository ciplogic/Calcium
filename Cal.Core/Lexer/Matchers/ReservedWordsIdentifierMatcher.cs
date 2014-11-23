using System;
using System.Collections.Generic;
using Cal.Core.NewLexer;

namespace Cal.Core.Lexer.Matchers
{
    public class ReservedWordsIdentifierMatcher : ExactWordsMatcherBase
    {
        private Dictionary<char, Tuple<char[], TokenKind>[]> _index;

        public ReservedWordsIdentifierMatcher()
        {
            var dict = BuildDictionary();
            AddReservedWord(dict, "class", TokenKind.RwClass);
            AddReservedWord(dict, "def", TokenKind.RwDef);
            AddReservedWord(dict, "end", TokenKind.RwEnd);
            AddReservedWord(dict, "load_assembly", TokenKind.RwLoadAssembly);
            AddReservedWord(dict, "new", TokenKind.RwNew);
            AddReservedWord(dict, "module", TokenKind.RwModule);
            AddReservedWord(dict, "if", TokenKind.RwIf);
            AddReservedWord(dict, "else", TokenKind.RwElse);
            AddReservedWord(dict, "while", TokenKind.RwWhile);
            AddReservedWord(dict, "self", TokenKind.RwSelf);
            AddReservedWord(dict, "do", TokenKind.RwDo);

            _index = BuildIndex(dict);
        }


        public static bool ComputeDoesMatch(string allText)
        {
            return CharUtils.IsAlpha(allText[0]);
        }

        public override MatchResult GetMatchResult(string allText)
        {
            if (!ComputeDoesMatch(allText))
                return MatchResult.None;

            var result = ComputeMatchResult(allText, _index);
            var matchIdentifier = IdentifierMatcher.ComputeMatchResult(allText);
         
            if (result.Equals(MatchResult.None))
            {
                return IdentifierMatcher.ComputeMatchResult(allText);
            }
            if (matchIdentifier.Length == result.Length) return result;
            return IdentifierMatcher.ComputeMatchResult(allText);
        }
    }
}