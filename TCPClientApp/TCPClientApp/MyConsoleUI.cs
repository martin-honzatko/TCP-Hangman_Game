using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClientApp
{
    public static class MyConsoleUI
    {
        internal static int ReadInt(string message)
        {
            string input;
            int result;

            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine();

            } while (!int.TryParse(input, out result));
            return result;
        }

        internal static Int32 ReadInt32(string message)
        {
            string input;
            Int32 result;

            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine();

            } while (!Int32.TryParse(input, out result));
            return result;
        }

        internal static string ReadString(string message)
        {
            string input;
            string result;

            Console.WriteLine(message);
            input = Console.ReadLine();
            result = input;
            return result;
        }

        internal static string ReadString(string message, string defaultInput)
        {
            string input;
            string result;

            Console.WriteLine(message);
            input = Console.ReadLine();

            if (input == String.Empty)
            {
                result = defaultInput;
                return result;
            }

            result = input;
            return result;
        }

        internal static bool ReadBool(string message)
        {
            string input;
            bool result;

            Console.WriteLine(message);
            input = Console.ReadLine();

            if (input == "Yes" || input == "Y" || input == "y")
            {
                result = true;
            }

            else
            {
                result = false;
            }

            return result;
        }
    }
}
