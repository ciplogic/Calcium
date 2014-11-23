using Cal.Core.Lexer;

namespace Cal.Core.NewLexer
{
    public abstract class LexerMatcher
    {
    	public abstract MatchResult GetMatchResult(string allText);
        public static bool QuickStartsWith(string text, char[] startChars)
        {
        	if(text.Length < startChars.Length)
        		return false;
            for (int i = 0; i < startChars.Length; i++)
            {
                var ch = startChars[i];
                if (text[i] != ch)
                    return false;
            }
            return true;
        }
        public static bool QuickStartsWith(string text, char startChar)
        {
            return text[0] == startChar;
        }
        public static int QuickIndexOf(string text, char ch, int startIndex)
        {
            for(var index = startIndex; index<text.Length;index++)
                if (text[index] == ch)
                    return index;
            return -1;
        }


        public static string ExtractMatchText(string text, int len)
        {
            return text.Substring(0, len);
        }
        public override string ToString()
        {
            return GetType().Name;
        }
    }
}