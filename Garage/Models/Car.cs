using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    internal class Car: Vehicle
    {
        int number_of_doors;
        public Car(string license_plate_number, string color, int model_year, FuelType fuel_type, int number_of_doors = 4)
            : base(license_plate_number, color, model_year, fuel_type)
        { 
            this.number_of_doors = number_of_doors;
        }

        public override string GetDescription()
        {
            return $"{base.GetDescription()}, number of doors: {number_of_doors}";
        }
    }
}
