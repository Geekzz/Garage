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
        string Color();
        int GetModelYear();
        FuelType FuelType();
        string GetDescription();
    }
}
