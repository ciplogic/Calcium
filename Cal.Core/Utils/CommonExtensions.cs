using System;
using System.Collections.Generic;

namespace Cal.Core.Utils
{
    public static class CommonExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static int IndexOfT<T>(this IEnumerable<T> collection, Predicate<T> predicate)
        {
            var pos = 0;
            foreach (var item in collection)
            {
                if (predicate(item))
                    return pos;
                pos++;
            }
            return -1;
        }
    }
}