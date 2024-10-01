using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Utils
{
    internal class Util
    {
        private static void PrintWarningTextColor(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(prompt);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static string AskForString(string prompt)
        {
            bool flag = false;
            string input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(input))
                {
                    PrintWarningTextColor("Your input is not valid");
                }
                else
                    flag = true;

            }while (!flag);

            return input;
        }

        public static uint AskForUInt(string prompt)
        {
            do
            {
                string input = AskForString(prompt);

                if(uint.TryParse(input, out uint result))
                    return result;

                PrintWarningTextColor("Invalid input type, please enter numeric input");

            } while(true);
        }
    }
}
