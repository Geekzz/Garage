using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    internal class Bus: Vehicle
    {
        public Bus(string license_plate_number, string color, uint model_year, FuelType fuel_type)
            : base(license_plate_number, color, model_year, fuel_type)
        { 

        }
    }
}
