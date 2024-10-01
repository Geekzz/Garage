﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    internal class Bus: Vehicle
    {
        private uint number_of_seats;
        public Bus(string license_plate_number, string color, uint model_year, FuelType fuel_type, uint number_of_seats)
            : base(license_plate_number, color, model_year, fuel_type)
        { 
            this.number_of_seats = number_of_seats;
        }
    }
}
