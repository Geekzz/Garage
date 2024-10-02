using Garage.Models;
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

        public static string AskForFuelType(string prompt)
        {
            string[] validFuelTypes = { "Gas", "Diesel", "Electric" }; // Valid options
            string? input;

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine(); // Read and trim input

                // Validate input against the valid fuel types
                if (Array.Exists(validFuelTypes, fuel => fuel.Equals(input, StringComparison.OrdinalIgnoreCase)))
                {
                    if(input != null)
                        return input; // Return valid input
                }
                else
                {
                    PrintWarningTextColor("Invalid fuel type. Please enter Gas, Diesel, or Electric");
                }
            }
        }

        public static string AskForColor(string prompt)
        {
            string[] validColors = { "Red", "Blue", "Green", "Yellow", "Black", "White" }; // Valid color options
            string? input;

            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine(); // Read and trim input

                // Validate input against the valid color options
                if (Array.Exists(validColors, color => color.Equals(input, StringComparison.OrdinalIgnoreCase)))
                {
                    if(input != null)
                        return char.ToUpper(input[0]) + input[1..].ToLower();
                }
                else
                {
                    PrintWarningTextColor("Invalid color. Please enter Red, Blue, Green, Yellow, Black, or White");
                }
            }
        }

        public static FuelType ConvertStringToFuelType(string valid_fuel_type)
        {
            if (valid_fuel_type.ToLower() == "gas")
                return FuelType.Gas;
            else if (valid_fuel_type.ToLower() == "diesel")
                return FuelType.Diesel;
            else
                return FuelType.Electric;
        }
    }
}
