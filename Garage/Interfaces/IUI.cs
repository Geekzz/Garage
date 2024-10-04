using Garage.Handlers;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Interfaces
{
    internal interface IUI
    {
        // If I want to use static methods on an interface, I need to use static + abstract
        // Reference: https://stackoverflow.com/questions/9415257/how-can-i-implement-static-methods-on-an-interface/60083436#60083436
        public static abstract void DisplayMenu();
        public static abstract void DisplaySuccessMessage(string message);
        public static abstract void DisplayWarningMessage(string message);
        public static abstract void AskVehicleInfo(uint vehicle, GarageHandler garageHandler);
        public static abstract void DisplayAllVehicles(GarageHandler garageHandler);
        public static abstract void DisplaySearchedVehicles(IEnumerable<Vehicle> vehicles);
        public static abstract string AskForStringInput(string prompt);
        public static abstract string AskUserForSearchVehicles();
        public static abstract uint AskVehicleType();
        public static abstract uint AskForUIntInput(string prompt);
    }
}
