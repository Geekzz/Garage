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
        bool RemoveVehicle(T vehicle);
        T GetVehicle(string register_plate_number);
    }
}
