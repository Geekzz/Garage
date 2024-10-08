﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    public class Motorcycle: Vehicle
    {
        uint engine_volume;

        public Motorcycle(string license_plate_number, string color, uint model_year, FuelType fuel_type, uint engine_volume)
            : base(license_plate_number, color, model_year, fuel_type)
        {
            this.engine_volume = engine_volume;
        }

        public override string GetDescription()
        {
            return string.Format("{0} {1,-10}", base.GetDescription(), $"Engine volume: {engine_volume}");
        }

        public uint GetEngimeVolume()
        {
            return engine_volume;
        }
    }
}
