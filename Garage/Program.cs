using Garage.Handlers;
using Garage.Models;
using Garage.UI;

namespace Garage
{
    internal class Program
    {
        static void AskVehicleToGarage(uint vehicle, GarageHandler garageHandler)
        {
            string license_num = ConsoleUI.AskLicensePlateNum();
            string color = ConsoleUI.AskVehicleColor();
            string fuel_type = ConsoleUI.AskFuelType();
            uint model_year = ConsoleUI.AskModelYear();

            switch (vehicle)
            {
                case 1:
                    uint door_num = ConsoleUI.AskNumberOfDoors();
                    Vehicle vh = new Car(license_num, color, model_year, FuelType.Diesel, door_num);
                    garageHandler.AddVehicle(vh);
                    ConsoleUI.AddedToGarageSuccess("Car");
                    break;
            }
        }
        static void Main(string[] args)
        {
            bool flag = true;
            GarageHandler? garageHandler = null;

            while (flag)
            {
                ConsoleUI.DisplayMenu();
                uint input = ConsoleUI.AskMenuOption();

                switch (input)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        uint size = ConsoleUI.AskGarageSize();
                        garageHandler = new GarageHandler(size);
                        break;
                    case 2:
                        if(garageHandler == null)
                        {
                            ConsoleUI.GarageIsEmpty();
                            break;
                        }
                        else if (garageHandler.GarageFull())
                        {
                            ConsoleUI.GarageIsFull();
                            break;
                        }
                        else
                        {
                            uint vehicle_type = ConsoleUI.AskVehicleType();
                            AskVehicleToGarage(vehicle_type, garageHandler);
                            break;
                        }
                }
            }
        }
    }
}
