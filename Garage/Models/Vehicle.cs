using Garage.Interfaces;
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

    internal class Vehicle: IVechile
    {
        private string? license_plate_number;
        private string? color;
        private FuelType fuel_type;
        private int model_year;

        public Vehicle(string license_plate_number, string color, int model_year, FuelType fuel_type)
        {
            this.license_plate_number = license_plate_number;
            this.color = color;
            this.model_year = model_year;
            this.fuel_type = fuel_type;
        }

        public string LicensePlateNumber() { return license_plate_number ?? string.Empty; }
        public string Color() { return color ?? string.Empty; }
        public FuelType FuelType() { return fuel_type; }

        public int GetModelYear() { return model_year; }

        public virtual string GetDescription()
        {
            return $"License plate number: {license_plate_number}, color: {color}, model year: {model_year}, fuel type: {fuel_type}";
        }
    }
}
