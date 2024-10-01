using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Util.PrintSuccessTextColor($"New garage with capacity of {capacity} added!");
        }

        public static void GarageIsEmpty()
        {
            Util.PrintWarningTextColor("There is no garage, please create one first");
        }

        public static void AddedToGarageSuccess(string vehicle)
        {
            Util.PrintSuccessTextColor($"{vehicle} have been added!");
        }

        public static void GarageIsFull()
        {
            Util.PrintWarningTextColor("Garage is full!");
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

        public static string AskLicensePlateNum()
        {
            return Util.AskForString("Enter license number, e.g. ABC444: ");
        }

        public static string AskVehicleColor()
        {
            return Util.AskForString("Enter color (e.g. Blue): ");
        }

        public static string AskFuelType()
        {
            return Util.AskForString("Enter fuel type (Gas, Diesel, or Electric): ");
        }

        public static uint AskModelYear()
        {
            return Util.AskForUInt("Enter model year: ");
        }

        public static uint AskNumberOfDoors()
        {
            return Util.AskForUInt("Enter number of doors: ");
        }

        public static void AskVehicleInfo(uint vehicle, GarageHandler garageHandler)
        {
            string license_num = AskLicensePlateNum();
            string color = AskVehicleColor();
            string fuel_type = AskFuelType();
            uint model_year = AskModelYear();

            Vehicle? vh = null; 

            switch (vehicle)
            {
                case 1:
                    uint door_num = AskNumberOfDoors();
                    vh = new Car(license_num, color, model_year, FuelType.Diesel, door_num);
                    garageHandler.AddVehicle(vh);
                    AddedToGarageSuccess("Car");
                    break;
                case 2:
                    uint engine_volume = Util.AskForUInt("Enter engine volume (e.g. 600): ");
                    vh = new Motorcycle(license_num, color, model_year, FuelType.Gas, engine_volume);
                    garageHandler.AddVehicle(vh);
                    AddedToGarageSuccess("Motorcycle");
                    break;
                case 3:
                    uint boat_length = Util.AskForUInt("Enter length of the boat: ");
                    vh = new Boat(license_num, color, model_year, FuelType.Diesel,  boat_length);
                    garageHandler.AddVehicle(vh);
                    AddedToGarageSuccess("Boat");
                    break;
                case 4:
                    uint seats_num = Util.AskForUInt("Enter number of passenger seats: ");
                    vh = new Bus(license_num, color, model_year, FuelType.Gas, seats_num);
                    garageHandler.AddVehicle(vh);
                    AddedToGarageSuccess("Bus");
                    break;
                case 5:
                    uint wings_span = Util.AskForUInt("Enter wings span length: ");
                    vh = new Airplane(license_num, color, model_year, FuelType.Gas, wings_span);
                    garageHandler.AddVehicle(vh);
                    AddedToGarageSuccess("Airplane");
                    break;
            }
        }
    }
}
