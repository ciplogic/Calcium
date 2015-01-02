using System;
using Cal.Core.Runtime;

namespace Cal.Runtime
{
    [GlobalFunctions]
    public class ConsoleMethods
    {
        public static void puts(string toDisplay)
        {
            Console.WriteLine(toDisplay);
        }
        public static void puts()
        {
            Console.WriteLine();
        }
        public static void print(string toDisplay)
        {
            Console.Write(toDisplay);
        }
    }
}