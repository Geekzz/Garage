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
    internal class GarageHandler<T> : IHandler<T> where T : Vehicle
    {
        private Garage<T> garage;

        public GarageHandler(int capacity)
        {
            // ny garage med dess storlek
            garage = new Garage<T>(capacity);
        }
        public bool AddVehicle(T vehicle)
        {
            throw new NotImplementedException();
        }

        public T GetVehicle(string register_plate_number)
        {
            // leta vehicle som matchar nummerplåt
            throw new NotImplementedException();
        }

        public bool RemoveVehicle(T vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
