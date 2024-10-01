using Garage.Garage;
using Garage.Interfaces;
using Garage.Models;
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
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public bool RemoveVehicle(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Vehicle GetVehicle(string register_plate_number)
        {
            // leta vehicle som matchar nummerplåt
            throw new NotImplementedException();
        }
    }
}
