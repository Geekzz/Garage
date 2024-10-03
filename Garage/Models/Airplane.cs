using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    internal class Airplane: Vehicle
    {
        private uint wings_span;
        public Airplane(string license_plate_number, string color, uint model_year, FuelType fuel_type, uint wings_span) 
            : base(license_plate_number, color, model_year, fuel_type)
        {
            this.wings_span = wings_span;
        }

        public override string GetDescription()
        {
            return string.Format("{0} {1,-10}", base.GetDescription(), $"Wings span: {wings_span}");
        }
    }
}
