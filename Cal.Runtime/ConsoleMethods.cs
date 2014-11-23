using System;

namespace Cal.Runtime
{
    [GlobalFunctions]
    public class ConsoleMethods
    {
        public static void puts(string toDisplay)
        {
            Console.WriteLine(toDisplay);
        }
    }
}