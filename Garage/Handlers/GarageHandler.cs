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
    }
}
