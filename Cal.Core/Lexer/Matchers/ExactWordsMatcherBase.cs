using System;
using System.Collections.Generic;
using Cal.Core.NewLexer;

namespace Cal.Core.Lexer.Matchers
{
    public abstract class ExactWordsMatcherBase : LexerMatcher
    {

        protected static MatchResult ComputeMatchResult(string allText, Dictionary<char, Tuple<char[], TokenKind>[]> indexToSearch)
        {
            Tuple<char[], TokenKind>[] possibleItems;
            if (!indexToSearch.TryGetValue(allText[0], out possibleItems))
                return MatchResult.None;
            foreach (var reservedWord in possibleItems)
            {
                if (!QuickStartsWith(allText, reservedWord.Item1)) continue;
                var result = new MatchResult(reservedWord.Item2, reservedWord.Item1.Length);
                return result;
            }
            return MatchResult.None;
        }

        public Dictionary<char, List<Tuple<string, TokenKind>>> BuildDictionary()
        {
            return new Dictionary<char, List<Tuple<string, TokenKind>>>();
        }

        protected static void AddReservedWord(Dictionary<char, List<Tuple<string, TokenKind>>> charsToSearch, 
            string identifier, TokenKind kind)
        {
            List<Tuple<string, TokenKind>> list;
            if (!charsToSearch.TryGetValue(identifier[0], out list))
            {
                list = new List<Tuple<string, TokenKind>>();
                charsToSearch[identifier[0]] = list;
            }
            list.Add(new Tuple<string, TokenKind>(identifier, kind));
        }

        protected Dictionary<char, Tuple<char[], TokenKind>[]> BuildIndex(Dictionary<char, List<Tuple<string, TokenKind>>> charsToSearch)
        {
            Dictionary<char, Tuple<char[], TokenKind>[]> _indexToSearch = new Dictionary<char, Tuple<char[], TokenKind>[]>();

            foreach (var item in charsToSearch)
            {
                var tuples = item.Value.ToArray();
                var indexTuple = new List<Tuple<char[], TokenKind> >();
                foreach (var tuple in tuples)
                {
                    indexTuple.Add(new Tuple<char[], TokenKind>(tuple.Item1.ToCharArray(), tuple.Item2));
                }
                _indexToSearch[item.Key] = indexTuple.ToArray();
            }
            return _indexToSearch;
        }
    }
}