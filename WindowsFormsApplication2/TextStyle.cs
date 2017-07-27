using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    static class TextStyle
    {
        public static void RedText(string text, params object[] post)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text, post);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void GreenText(string text, params object[] post)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text, post);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
