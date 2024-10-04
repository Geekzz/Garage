using Garage.Garage;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Interfaces
{
    internal interface IHandler<T>
    {
        bool AddVehicle(T vehicle);
        bool RemoveVehicle(string register_plate_number);
        bool GarageFull();
        public List<Garage<Vehicle>> GetGarageList();
        public Dictionary<string, int> GetVehiclesTypes();
        public IEnumerable<Vehicle> GetGarage();
        public IEnumerable<Vehicle> SearchVehicles(string input);
    }
}
