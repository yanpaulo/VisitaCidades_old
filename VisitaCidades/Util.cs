using System;
using System.Collections.Generic;
using System.Text;

namespace VisitaCidades
{
    public class Util
    {
        public static void ColoredPrint(string str, ConsoleColor foreground = ConsoleColor.Black, ConsoleColor background = ConsoleColor.Cyan)
        {
            try
            {
                Console.BackgroundColor = background;
                Console.ForegroundColor = foreground;
                Console.Write(str);
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
