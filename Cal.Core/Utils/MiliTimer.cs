#region Usages

using System;
using System.Collections.Generic;

#endregion

namespace Cal.Core.Utils
{
    public class MiliTimer
    {
        private int _end;
        private int _start;

        public MiliTimer()
        {
            _start = Environment.TickCount;
        }

        public void ShowFromStart()
        {
            _end = Environment.TickCount;
            Console.WriteLine("Time from start: " + (_end - _start) + " ms.");
        }

        public void ShowAndReset()
        {
            ShowFromStart();
            _start = Environment.TickCount;
        }
    }

    public static class CommonExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}