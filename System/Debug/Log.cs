using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.System.Debug
{
    public static class Log
    {
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[WARNING] ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[SUCCESS] ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("[INFO] ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public static string Center(string message)
        {
            int width = Console.WindowWidth;
            int padd = (width - message.Length) / 2;
            string cText = message.PadLeft(padd + message.Length).PadRight(width);
            return cText;
        }
    }
}
