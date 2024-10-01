using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage.Utils;

namespace Garage.UI
{
    internal static class ConsoleUI
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("0. Exit program");
            Console.WriteLine("1. Create garage");
            Console.WriteLine("2. Add vehicle to the garage");
            Console.WriteLine("3. Remove vehicle from the garage");
        }

        public static uint AskMenuOption()
        {
            return Util.AskForUInt("Menu option: ");
        }

        public static uint AskGarageSize()
        {
            return Util.AskForUInt("Enter garage's capacity: ");
        }
        public static void DisplayGarageAdded(uint capacity)
        {
            Console.WriteLine($"New garage with capacity of {capacity} added!");
        }

        public static void AskVehicleType()
        {
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcyle");
            Console.WriteLine("3. Boat");
            Console.WriteLine("4. Bus");
            Console.WriteLine("5. Airplane");
        }
    }
}
