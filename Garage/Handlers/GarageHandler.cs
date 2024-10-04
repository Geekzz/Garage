﻿using Garage.Garage;
using Garage.Interfaces;
using Garage.Models;
using Garage.UI;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ReadGarageJsonFile(IConfiguration config)
        {
            var garages = config.GetSection("garages").GetChildren();

            foreach (var garage in garages)
            {
                // Parse garage capacity
                if (uint.TryParse(garage["capacity"], out uint capacity))
                {
                    // Create a new garage instance with the given capacity
                    Garage<Vehicle> newGarage = new Garage<Vehicle>(capacity);

                    // Read vehicles from JSON and add them to the garage
                    var vehicles = garage.GetSection("vehicles").GetChildren();
                    foreach (var vehicle in vehicles)
                    {
                        Vehicle newVehicle = null;

                        // Handle different types of vehicles based on the "type" field
                        switch (vehicle["type"])
                        {
                            case "Car":
                                newVehicle = new Car(
                                    vehicle["licensePlate"],
                                    vehicle["color"],
                                    uint.Parse(vehicle["modelYear"]),
                                    Utils.Util.ConvertStringToFuelType(vehicle["fuelType"]),
                                    uint.Parse(vehicle["numberOfDoors"])
                                );
                                break;

                            case "Motorcycle":
                                newVehicle = new Motorcycle(
                                    vehicle["licensePlate"],
                                    vehicle["color"],
                                    uint.Parse(vehicle["modelYear"]),
                                    Utils.Util.ConvertStringToFuelType(vehicle["fuelType"]),
                                    uint.Parse(vehicle["engineVolume"])
                                );
                                break;

                            case "Boat":
                                newVehicle = new Boat(
                                    vehicle["licensePlate"],
                                    vehicle["color"],
                                    uint.Parse(vehicle["modelYear"]),
                                    Utils.Util.ConvertStringToFuelType(vehicle["fuelType"]),
                                    uint.Parse(vehicle["length"])
                                );
                                break;

                            case "Bus":
                                newVehicle = new Bus(
                                    vehicle["licensePlate"],
                                    vehicle["color"],
                                    uint.Parse(vehicle["modelYear"]),
                                    Utils.Util.ConvertStringToFuelType(vehicle["fuelType"]),
                                    uint.Parse(vehicle["numberOfSeats"])
                                );
                                break;
                        }

                        // Add the new vehicle to the garage
                        if (newVehicle != null)
                        {
                            newGarage.AddVehicle(newVehicle);
                        }
                    }

                    // Add the new garage to the garage list
                    garage_list.Add(newGarage);
                }
            }
        }
    }
}
