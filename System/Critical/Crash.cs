using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.System.Critical
{
    public static class Crash
    {
        public static void Message(Exception ex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error!");

            Console.ResetColor();
            Console.WriteLine(ex + "\n");

            Console.WriteLine("Please report this error on the message board.");
        }
    }
}
