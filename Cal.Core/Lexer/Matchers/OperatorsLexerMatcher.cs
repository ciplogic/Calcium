using System;
using System.Collections.Generic;
using Cal.Core.NewLexer.Matchers;

namespace Cal.Core.Lexer.Matchers
{
    public class OperatorsLexerMatcher : ExactWordsMatcherBase
    {
        private Dictionary<char, Tuple<char[], TokenKind>[]> _index;
        public OperatorsLexerMatcher()
        {
            var dict = BuildDictionary();
            AddReservedWord(dict, ".", TokenKind.OpDot);
            AddReservedWord(dict, "==", TokenKind.OpEquals);
            AddReservedWord(dict, "+=", TokenKind.OpAddBy);
            AddReservedWord(dict, "-=", TokenKind.OpSubBy);

            AddReservedWord(dict, "+", TokenKind.OpAdd);
            AddReservedWord(dict, "-", TokenKind.OpSub);
            AddReservedWord(dict, "*", TokenKind.OpMul);
            AddReservedWord(dict, "/", TokenKind.OpDiv);

            AddReservedWord(dict, "&&", TokenKind.OpBoolAnd);
            AddReservedWord(dict, "||", TokenKind.OpBoolOr);

            
            AddReservedWord(dict, "!=", TokenKind.OpNotEqual);
            AddReservedWord(dict, ">=", TokenKind.OpGreaterThan);
            AddReservedWord(dict, "<=", TokenKind.OpLessThan);
            AddReservedWord(dict, "<", TokenKind.OpLessStrict);
            AddReservedWord(dict, ">", TokenKind.OpGreaterStrict);

            AddReservedWord(dict, "(", TokenKind.OpOpenParen);
            AddReservedWord(dict, ")", TokenKind.OpCloseParen);
            AddReservedWord(dict, "[", TokenKind.OpOpenSquareParen);
            AddReservedWord(dict, "]", TokenKind.OpCloseSquareParen);
            AddReservedWord(dict, "{", TokenKind.OpOpenCurlyParen);
            AddReservedWord(dict, "}", TokenKind.OpCloseCurlyParen);

            AddReservedWord(dict, "=", TokenKind.OpAssign);
            AddReservedWord(dict, "<<", TokenKind.OpOutStream);
            AddReservedWord(dict, "::", TokenKind.OpResolution);
            AddReservedWord(dict, ":", TokenKind.OpColumn);

            AddReservedWord(dict, "|", TokenKind.OpPipe);
            AddReservedWord(dict, ",", TokenKind.OpComma);
            AddReservedWord(dict, ";", TokenKind.OpSemiColon);

            AddReservedWord(dict, "!", TokenKind.OpBoolNot);
            AddReservedWord(dict, "&", TokenKind.OpBoolAnd);

            AddReservedWord(dict, "?", TokenKind.OpIf);
            AddReservedWord(dict, ":", TokenKind.OpElse);

            _index = BuildIndex(dict);
        }

        public override MatchResult GetMatchResult(string allText)
        {
            return ComputeMatchResult(allText, _index);
        }

    }
}