using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Garage.Garage;
using Garage.Handlers;
using Garage.Interfaces;
using Garage.Models;
using Garage.Utils;

namespace Garage.UI
{
    internal class ConsoleUI: IUI
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("0. Exit program");
            Console.WriteLine("1. Create garage");
            Console.WriteLine("2. Add vehicle to the garage");
            Console.WriteLine("3. Remove vehicle from the garage");
            Console.WriteLine("4. Display all vehicles from the garage");
            Console.WriteLine("5. Search for vehicles based on information");
        }

        public static uint AskForUIntInput(string prompt)
        {
            return Util.AskForUInt(prompt);
        }

        public static string AskForStringInput(string prompt)
        {
            return Util.AskForString(prompt);
        }

        public static string AskForRegNum(string prompt)
        {
            return Util.AskForRegPlateNumber(prompt);
        }

        public static void DisplaySuccessMessage(string message)
        {
            Console.Clear();
            Util.PrintSuccessTextColor(message);
        }

        public static void DisplayWarningMessage(string message)
        {
            Console.Clear();
            Util.PrintWarningTextColor(message);
        }

        public static uint AskVehicleType()
        {
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Motorcyle");
            Console.WriteLine("3. Boat");
            Console.WriteLine("4. Bus");
            Console.WriteLine("5. Airplane");

            while (true)
            {
                uint input = Util.AskForUInt("Enter car type (1-5): ");
                if (input >= 1 && input <= 5)
                    return input;
                Util.PrintWarningTextColor("Please enter 1-5!");
            }
        }

        public static void AskVehicleInfo(uint vehicle, GarageHandler garageHandler)
        {
            string license_num = Util.AskForRegPlateNumber("Enter license number, e.g. ABC444: ");
            string color = Util.AskForColor("Choose color: ");
            string fuel_type = Util.AskForFuelType("Enter fuel type: ");
            uint model_year = Util.AskForNumberInRange("Enter model year: ", 1900, 2024);

            Vehicle? vh = null; 

            switch (vehicle)
            {
                case 1:
                    uint door_num = Util.AskForNumberInRange("Enter number of doors: ", 1, 4);
                    vh = new Car(license_num, color, model_year, Util.ConvertStringToFuelType(fuel_type), door_num);
                    garageHandler.AddVehicle(vh);
                    DisplaySuccessMessage("Car added successfully!");
                    break;
                case 2:
                    uint engine_volume = Util.AskForNumberInRange("Enter engine volume (e.g. 600): ", 50, 1300);
                    vh = new Motorcycle(license_num, color, model_year, Util.ConvertStringToFuelType(fuel_type), engine_volume);
                    garageHandler.AddVehicle(vh);
                    DisplaySuccessMessage("Motorcycle added successfully!");
                    break;
                case 3:
                    uint boat_length = Util.AskForNumberInRange("Enter length of the boat: ", 1, 50);
                    vh = new Boat(license_num, color, model_year, Util.ConvertStringToFuelType(fuel_type),  boat_length);
                    garageHandler.AddVehicle(vh);
                    DisplaySuccessMessage("Boat added successfully!");
                    break;
                case 4:
                    uint seats_num = Util.AskForNumberInRange("Enter number of passenger seats: ", 6, 20);
                    vh = new Bus(license_num, color, model_year, Util.ConvertStringToFuelType(fuel_type), seats_num);
                    garageHandler.AddVehicle(vh);
                    DisplaySuccessMessage("Bus added successfully!");
                    break;
                case 5:
                    uint wings_span = Util.AskForNumberInRange("Enter wings span length: ", 2, 20);
                    vh = new Airplane(license_num, color, model_year, Util.ConvertStringToFuelType(fuel_type), wings_span);
                    garageHandler.AddVehicle(vh);
                    DisplaySuccessMessage("Airplane added successfully!");
                    break;
            }
        }

        public static void DisplayAllVehicles(GarageHandler garageHandler)
        {

            // Check if there are any garages
            if (garageHandler.GetGarageList().Count == 0)
            {
                DisplayWarningMessage("There's no garage avaiable");
                return;
            }
            Console.Clear();

            int index = 1;
            // Loop through each garage in the garage list
            foreach (var garage in garageHandler.GetGarageList())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Garage number {index} with a capacity of: {garage.GetCapacity()}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(new string('-', 100));

                // Print table headers
                Console.WriteLine(
                    $"{"Vehicle Type",-15} " +
                    $"{"License Plate",-20} " +
                    $"{"Color",-10} " +
                    $"{"Year",-6} " +
                    $"{"Fuel type",-20} " +
                    $"{"Description",-10}");

                Console.WriteLine(new string('-', 100)); 

                // Loop through vehicles in the current garage
                var vehicles = garage.GetEnumerator(); // Use the enumerator to iterate through the vehicles

                while (vehicles.MoveNext())
                {
                    var vehicle = vehicles.Current;

                    if (vehicle != null)
                    {
                        // Print the vehicle details if not null
                        Console.WriteLine($"{vehicle.GetType().Name,-15} {vehicle.GetDescription()}");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"{"-empty-",-15} " +
                            $"{"-empty-",-20} " +
                            $"{"-empty-",-10} " +
                            $"{"-empty-",-6} " +
                            $"{"-empty-",-20} " +
                            $"{"-empty-",-10}"); 
                    }
                }

                Console.WriteLine(new string('-', 100));
                index++;
            }

            // Get vehicle type counts across all garages
            var vehicleTypeCounts = garageHandler.GetVehiclesTypes();

            // Display vehicle type counts below the table
            Console.WriteLine("Vehicle Type Summary:");
            foreach (var kvp in vehicleTypeCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}"); // Display type and its count
            }

            Console.ForegroundColor = ConsoleColor.Gray; // Reset the text color to gray after printing
            Console.WriteLine(); // Print a new line after the entire table
        }



        public static string AskUserForSearchVehicles()
        {
            Util.PrintSuccessTextColor("\nNote: You can use spaces to separate multiple search terms");
            return Util.AskForString("Enter your search criteria for vehicles (e.g., color, license plate, etc.): ");
        }

        public static void DisplaySearchedVehicles(IEnumerable<Vehicle> vehicles)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(
                $"{"Vehicle Type",-15} " +
                $"{"License Plate",-20} " +
                $"{"Color",-10} " +
                $"{"Year",-6} " +
                $"{"Fuel type",-20} " +
                $"{"Description",-10}");

            Console.WriteLine(new string('-', 100)); // Separator line

            // Loop through the vehicles to display their details
            foreach (var vehicle in vehicles)
            {
                if (vehicle != null)
                {
                    // Print the vehicle details if not null
                    Console.WriteLine($"{vehicle.GetType().Name,-15} {vehicle.GetDescription()}");
                }
                else
                {
                    // Print empty spot indicator for consistency in case the list has empty slots
                    Console.WriteLine(
                        $"{"-empty-",-15} " +
                        $"{"-empty-",-20} " +
                        $"{"-empty-",-10} " +
                        $"{"-empty-",-6} " +
                        $"{"-empty-",-20}");
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray; // Reset the text color to gray after printing
            Console.WriteLine(); // Print a new line after the entire table
        }
    }
}
