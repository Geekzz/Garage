using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    public class Car: Vehicle
    {
        uint number_of_doors;
        public Car(string license_plate_number, string color, uint model_year, FuelType fuel_type, uint number_of_doors = 4)
            : base(license_plate_number, color, model_year, fuel_type)
        { 
            this.number_of_doors = number_of_doors;
        }

        public override string GetDescription()
        {
            return string.Format("{0} {1,-10}", base.GetDescription(), $"Number of doors: {number_of_doors}");
        }

        public uint GetNumOfDoors()
        {
            return number_of_doors;
        }
    }
}
