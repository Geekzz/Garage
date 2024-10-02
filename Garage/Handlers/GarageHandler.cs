using Garage.Garage;
using Garage.Interfaces;
using Garage.Models;
using Garage.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Handlers
{
    internal class GarageHandler: IHandler<Vehicle>
    {
        private Garage<Vehicle> garage;

        public GarageHandler(uint capacity)
        {
            // ny garage med dess storlek
            garage = new Garage<Vehicle>(capacity);
            ConsoleUI.DisplaySuccessMessage($"Garage with capacity of {capacity} added successfully");
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            return garage.AddVehicle(vehicle);
        }

        public bool RemoveVehicle(string register_plate_number)
        {
            return garage.RemoveVehicle(register_plate_number);
        }

        public bool GarageFull()
        {
            return garage.IsGarageFull();
        }

        public IEnumerable<Vehicle> GetGarage()
        {
            return garage;
        }

        public Dictionary<string, int> GetVehiclesTypes()
        {
            Dictionary<string, int> vehicles_dic = new Dictionary<string, int>();

            foreach (Vehicle vehicle in garage)
            {
                // Check if the vehicle is not null to prevent error
                if (vehicle != null)
                {
                    var vehicle_type = vehicle.GetType().Name;
                    if (vehicles_dic.ContainsKey(vehicle_type))
                        vehicles_dic[vehicle_type]++;
                    else
                        vehicles_dic[vehicle_type] = 1;
                }
            }

            return vehicles_dic;
        }

        public IEnumerable<Vehicle> SearchVehicles(string input)
        {
            // Split the input by spaces to get individual search terms
            var searchTerms = input.Split(" ");

            // Use LINQ to filter vehicles based on the search terms
            var matchingVehicles = garage
                .Where(vehicle => vehicle != null &&
                                 searchTerms.Any(term =>
                                     vehicle.GetColor().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                     vehicle.LicensePlateNumber().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                     vehicle.GetDescription().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                     vehicle.GetModelYear().ToString().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                                     Enum.GetName(typeof(FuelType), vehicle.FuelType()).Equals(term, StringComparison.OrdinalIgnoreCase) ||
                                     vehicle.GetType().Name.Contains(term, StringComparison.OrdinalIgnoreCase)))
                .ToList();


            return matchingVehicles; // Return the matching vehicles
        }
    }
}
