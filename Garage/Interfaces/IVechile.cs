using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Interfaces
{
    internal interface IVechile
    {
        string LicensePlateNumber();
        string GetColor();
        string GetDescription();
        uint GetModelYear();
        FuelType FuelType();
    }
}
