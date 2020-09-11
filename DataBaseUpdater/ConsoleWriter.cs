using System;

namespace DataBaseUpdater
{
    public class ConsoleWriter
    {
        public static void InfoLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        public static void ErrorLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        public static void WarningLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }
    }
}