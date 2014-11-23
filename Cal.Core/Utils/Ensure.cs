#region Usages

using System;

#endregion

namespace Cal.Utils
{
    internal static class Ensure
    {
        public static void AreEqual<T>(T a, T b)
        {
            if (!Equals(a, b))
                throw new Exception("Not equal");
        }
    }
}