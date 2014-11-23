#region Usages

using System.Collections.Generic;

#endregion

namespace Cal.Core.NewLexer
{
    internal static class CharUtils
    {
        public static bool IsDigit(char data)
        {
            return (data >= '0') && (data <= '9');
        }

        public static bool IsAlpha(char data)
        {
            return (data >= 'a' && data <= 'z')
                   || (data >= 'A' && data <= 'Z')
                   || (data == '_') || (data == '@');
        }
    }
}