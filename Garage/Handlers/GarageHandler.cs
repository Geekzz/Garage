using Garage.Garage;
using Garage.Interfaces;
using Garage.Models;
using Garage.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

                // Iterate through each line and print the contents
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine($"The file {path} does not exist.");
            }
        }

        public void WriteToGarageJsonFile()
        {
            // Use the current directory for file path
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.txt");
            StringBuilder sb = new StringBuilder();

            // Garage 1
            sb.Append("Capacity: 10, Vehicles: [");
            sb.Append("{Type: Car, LicensePlate: ABC123, Color: Red, ModelYear: 2020, FuelType: Gasoline, NumberOfDoors: 4}, ");
            sb.Append("{Type: Motorcycle, LicensePlate: XYZ456, Color: Blue, ModelYear: 2018, FuelType: Gas, EngineVolume: 600}");
            sb.AppendLine("]");

            // Garage 2
            sb.Append("Capacity: 5, Vehicles: [");
            sb.Append("{Type: Boat, LicensePlate: BOAT789, Color: White, ModelYear: 2015, FuelType: Diesel, Length: 25}, ");
            sb.Append("{Type: Bus, LicensePlate: BUS012, Color: Yellow, ModelYear: 2019, FuelType: Diesel, NumberOfSeats: 20}");
            sb.AppendLine("]");

            // Write the content to the file (create it if it doesn't exist)
            File.WriteAllText(path, sb.ToString());

            // Inform the user (optional)
            Console.WriteLine($"Data has been written to {path}");
        }
    }
}
