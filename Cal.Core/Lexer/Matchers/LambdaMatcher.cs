using System;
using Cal.Core.NewLexer;

namespace Cal.Core.Lexer.Matchers
{
    public abstract class LambdaMatcher : LexerMatcher
    {
        public Func<string, bool> DoesMatchFunc;

        public Func<string, MatchResult> DoesMatchLength;

        public LambdaMatcher(Func<string, bool> doesMatchFunc, Func<string, MatchResult> doesMatchLength)
        {
            DoesMatchFunc = doesMatchFunc;
            DoesMatchLength = doesMatchLength;
        }

        public override MatchResult GetMatchResult(string allText)
        {
            if (!DoesMatchFunc(allText))
                return MatchResult.None;
            return DoesMatchLength(allText);
        }
    }
}