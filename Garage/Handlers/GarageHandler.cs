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
    internal class GarageHandler
    {
        private Garage<Vehicle> garage;

        public GarageHandler(uint capacity)
        {
            // ny garage med dess storlek
            garage = new Garage<Vehicle>(capacity);
            ConsoleUI.DisplayGarageAdded(capacity);
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

        public Vehicle GetVehicle(string register_plate_number)
        {
            // leta vehicle som matchar nummerplåt
            throw new NotImplementedException();
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

    }
}
