using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    internal class Boat: Vehicle
    {
        uint length_size;
        public Boat(string license_plate_number, string color, uint model_year, FuelType fuel_type, uint length_size)
            : base(license_plate_number, color, model_year, fuel_type)
        {
            this.length_size = length_size;
        }

        public override string GetDescription()
        {
            return string.Format("{0} {1,-10}", base.GetDescription(), $"Length: {length_size}");
        }

        public uint GetLengthSize()
        {
            return length_size;
        }
    }
}
