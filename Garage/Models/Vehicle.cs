﻿using Garage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    public enum FuelType
    {
        Gas,
        Diesel, 
        Electric
    }

    public class Vehicle: IVechile
    {
        private string? license_plate_number;
        private string? color;
        private FuelType fuel_type;
        private uint model_year;

        public Vehicle(string license_plate_number, string color, uint model_year, FuelType fuel_type)
        {
            this.license_plate_number = license_plate_number;
            this.color = color;
            this.model_year = model_year;
            this.fuel_type = fuel_type;
        }

        public string LicensePlateNumber() { return license_plate_number ?? string.Empty; }
        public string GetColor() { return color ?? string.Empty; }
        public FuelType FuelType() { return fuel_type; }

        public uint GetModelYear() { return model_year; }

        public virtual string GetDescription()
        {
            return string.Format("{0,-20} {1,-10} {2,-6} {3,-20}", license_plate_number, color, model_year, fuel_type);
        }
    }
}
