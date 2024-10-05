using Garage.Garage;
using Garage.Interfaces;
using Garage.Models;
using Garage.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Garage.Handlers
{
    internal class GarageHandler : IHandler<Vehicle>
    {
        private readonly List<Garage<Vehicle>> garage_list;

        public GarageHandler()
        {
            garage_list = new List<Garage<Vehicle>>();
            //Garage<Vehicle> garage = new Garage<Vehicle>(capacity);
            //garage_list.Add(garage);
            //ConsoleUI.DisplaySuccessMessage($"Garage with capacity of {capacity} added successfully");
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            foreach (var garage in garage_list)
            {
                // Try to add the vehicle to the current garage
                if (!garage.IsGarageFull())
                {
                    if (garage.AddVehicle(vehicle))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void AddGarage(uint capacity)
        {
            Garage<Vehicle> newGarage = new Garage<Vehicle>(capacity);
            garage_list.Add(newGarage);
            ConsoleUI.DisplaySuccessMessage($"Garage with capacity of {capacity} added successfully");
        }

        public List<Garage<Vehicle>> GetGarageList()
        {
            return garage_list;
        }

        public bool RemoveVehicle(string register_plate_number)
        {
            foreach (var garage in garage_list)
            {
                if (garage.RemoveVehicle(register_plate_number))
                {
                    return true;
                }
            }

            return false;
            //return garage.RemoveVehicle(register_plate_number);
        }

        public bool GarageFull()
        {
            return garage_list.All(g => g.IsGarageFull());
            //return garage.IsGarageFull();
        }


        public IEnumerable<Vehicle> GetGarage()
        {
            return garage_list.SelectMany(g => g);
            //return garage;
        }

        public Dictionary<string, int> GetVehiclesTypes()
        {
            Dictionary<string, int> vehicles_dic = new Dictionary<string, int>();

            // Loop through all garages in the list
            foreach (var garage in garage_list)
            {
                // Loop through each vehicle in the current garage
                foreach (Vehicle vehicle in garage)
                {
                    // Check if the vehicle is not null to prevent error
                    if (vehicle != null)
                    {
                        var vehicle_type = vehicle.GetType().Name;

                        // If the vehicle type exists, increment the count, otherwise add it to the dictionary
                        if (vehicles_dic.TryGetValue(vehicle_type, out int value))
                            vehicles_dic[vehicle_type] = ++value;
                        else
                            vehicles_dic[vehicle_type] = 1;
                    }
                }
            }

            return vehicles_dic;
        }

        public IEnumerable<Vehicle> SearchVehicles(string input)
        {
            // Split the input by spaces to get individual search terms
            var searchTerms = input.Split(" ");

            // Create a list to hold all matching vehicles
            var matchingVehicles = new List<Vehicle>();

            // Loop through each garage in the garage list
            foreach (var garage in garage_list)
            {
                // Use LINQ to filter vehicles based on the search terms for each garage
                var garageMatchingVehicles = garage
                    .Where(vehicle => vehicle != null &&
                                     searchTerms.Any(term =>
                                         vehicle.GetColor().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                         vehicle.LicensePlateNumber().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                         vehicle.GetDescription().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                         vehicle.GetModelYear().ToString().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                         Enum.GetName(typeof(FuelType), vehicle.FuelType()).Equals(term, StringComparison.OrdinalIgnoreCase) ||
                                         vehicle.GetType().Name.Contains(term, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                // Add matching vehicles from this garage to the main list
                matchingVehicles.AddRange(garageMatchingVehicles);
            }

            return matchingVehicles; // Return the matching vehicles
        }

        public void ReadGarageJsonFile()
        {
            // Use the current directory to find the appsettings.txt file
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.txt");

            // Check if the file exists before attempting to read it
            if (File.Exists(path))
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(path);

                // Iterate through each line and parse the contents
                foreach (var line in lines)
                {
                    // Parse the capacity part
                    var capacityMatch = Regex.Match(line, @"Capacity:\s*(\d+),");

                    if (capacityMatch.Success && uint.TryParse(capacityMatch.Groups[1].Value, out uint capacity))
                    {
                        // Create a new garage with the parsed capacity
                        Garage<Vehicle> newGarage = new Garage<Vehicle>(capacity);

                        // Parse the vehicles part (extract the content inside the brackets [])
                        var vehiclesMatch = Regex.Match(line, @"Vehicles:\s*\[(.*)\]");

                        if (vehiclesMatch.Success)
                        {
                            // Split the vehicle data (comma-separated)
                            string vehiclesData = vehiclesMatch.Groups[1].Value;
                            string[] vehiclesArray = vehiclesData.Split(new string[] { "}, {" }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var vehicleData in vehiclesArray)
                            {
                                // Add curly braces back to make parsing easier
                                string formattedVehicleData = vehicleData.Replace("{", "").Replace("}", "").Trim();

                                // Parse the individual vehicle fields
                                Dictionary<string, string> vehicleDict = new Dictionary<string, string>();
                                foreach (var field in formattedVehicleData.Split(','))
                                {
                                    var keyValue = field.Split(':');
                                    if (keyValue.Length == 2)
                                    {
                                        vehicleDict[keyValue[0].Trim()] = keyValue[1].Trim();
                                    }
                                }

                                // Create the appropriate vehicle object based on the parsed data
                                Vehicle newVehicle = null;
                                switch (vehicleDict["Type"])
                                {
                                    case "Car":
                                        newVehicle = new Car(
                                            vehicleDict["LicensePlate"],
                                            vehicleDict["Color"],
                                            uint.Parse(vehicleDict["ModelYear"]),
                                            Utils.Util.ConvertStringToFuelType(vehicleDict["FuelType"]),
                                            uint.Parse(vehicleDict["NumberOfDoors"])
                                        );
                                        break;

                                    case "Motorcycle":
                                        newVehicle = new Motorcycle(
                                            vehicleDict["LicensePlate"],
                                            vehicleDict["Color"],
                                            uint.Parse(vehicleDict["ModelYear"]),
                                            Utils.Util.ConvertStringToFuelType(vehicleDict["FuelType"]),
                                            uint.Parse(vehicleDict["EngineVolume"])
                                        );
                                        break;

                                    case "Boat":
                                        newVehicle = new Boat(
                                            vehicleDict["LicensePlate"],
                                            vehicleDict["Color"],
                                            uint.Parse(vehicleDict["ModelYear"]),
                                            Utils.Util.ConvertStringToFuelType(vehicleDict["FuelType"]),
                                            uint.Parse(vehicleDict["Length"])
                                        );
                                        break;

                                    case "Bus":
                                        newVehicle = new Bus(
                                            vehicleDict["LicensePlate"],
                                            vehicleDict["Color"],
                                            uint.Parse(vehicleDict["ModelYear"]),
                                            Utils.Util.ConvertStringToFuelType(vehicleDict["FuelType"]),
                                            uint.Parse(vehicleDict["NumberOfSeats"])
                                        );
                                        break;
                                }

                                // Add the new vehicle to the garage
                                if (newVehicle != null)
                                {
                                    newGarage.AddVehicle(newVehicle);
                                }
                            }
                        }

                        // Add the newly created garage to the list
                        garage_list.Add(newGarage);
                    }
                }
                Console.Clear();
                Utils.Util.PrintSuccessTextColor("The file is loaded successfully!");
            }
            else
            {
                Console.Clear();
                Utils.Util.PrintWarningTextColor($"The file {path} does not exist.");
                //Console.WriteLine($"The file {path} does not exist.");
            }
        }


        public void WriteToGarageJsonFile()
        {
            // Use the current directory for file path
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.txt");
            StringBuilder sb = new StringBuilder();

            // Iterate through each garage in the garage_list
            foreach (var garage in garage_list)
            {
                // Write the capacity of the garage
                sb.Append($"Capacity: {garage.GetCapacity()}, Vehicles: [");

                int vehicleCount = 0; // Counter to keep track of vehicles

                // Iterate through each vehicle in the garage
                for (int i = 0; i < garage.GetCapacity(); i++)
                {
                    var vehicle = garage.GetVehicle(i);

                    if (vehicle != null)
                    {
                        // Check the type of vehicle and append its properties
                        if (vehicle is Car car)
                        {
                            sb.Append($"{{Type: Car, LicensePlate: {car.LicensePlateNumber()}, Color: {car.GetColor()}, ModelYear: {car.GetModelYear()}, FuelType: {car.FuelType()}, NumberOfDoors: {car.GetNumOfDoors()}}}");
                        }
                        else if (vehicle is Motorcycle motorcycle)
                        {
                            sb.Append($"{{Type: Motorcycle, LicensePlate: {motorcycle.LicensePlateNumber()}, Color: {motorcycle.GetColor()}, ModelYear: {motorcycle.GetModelYear()}, FuelType: {motorcycle.FuelType()}, EngineVolume: {motorcycle.GetEngimeVolume()}}}");
                        }
                        else if (vehicle is Boat boat)
                        {
                            sb.Append($"{{Type: Boat, LicensePlate: {boat.LicensePlateNumber()} , Color:  {boat.GetColor()} , ModelYear:  {boat.GetModelYear()} , FuelType:  {boat.FuelType()}, Length: {boat.GetLengthSize()}}}");
                        }
                        else if (vehicle is Bus bus)
                        {
                            sb.Append($"{{Type: Bus, LicensePlate: {bus.LicensePlateNumber()} , Color:  {bus.GetColor()} , ModelYear:  {bus.GetModelYear()} , FuelType:  {bus.FuelType()}, NumberOfSeats: {bus.GetNumberOfSeats()}}}");
                        }
                        else if (vehicle is Airplane airplane)
                        {
                            sb.Append($"{{Type: Bus, LicensePlate: {airplane.LicensePlateNumber()} , Color:  {airplane.GetColor()} , ModelYear:  {airplane.GetModelYear()} , FuelType:  {airplane.FuelType()}, NumberOfSeats: {airplane.GetWingsSpan()}}}");
                        }

                        // Add a comma and space after each vehicle except the last one
                        if (vehicleCount < garage.GetCapacity() && garage.GetVehicle(vehicleCount) != null)
                        {
                            sb.Append(", ");
                        }
                    }
                }

                // Close the vehicle array and add a new line
                sb.AppendLine("]");
            }

            // Write the content to the file (create it if it doesn't exist)
            File.WriteAllText(path, sb.ToString());

            // Inform the user (optional)
            Console.Clear();
            Utils.Util.PrintSuccessTextColor($"Data has been written to {path}");
            //Console.WriteLine($"Data has been written to {path}");
        }

    }
}
