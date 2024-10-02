using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Garage.Utils
{
    public class Util
    {
        public static void PrintWarningTextColor(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(prompt);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintSuccessTextColor(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
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

        public static string AskForRegPlateNumber(string prompt)
        {
            string patterns = "^[A-Z]{3}[0-9]{3}";
            Regex regex = new Regex(patterns);
            string input;

            do
            {
                input = AskForString(prompt).ToUpper();

                if (!regex.IsMatch(input))
                    PrintWarningTextColor("Invalid register plate number format, please enter 3 letters followed by 3 numbers");
                else
                    break;
            } while (true);

            return input;
        }
    }
}
