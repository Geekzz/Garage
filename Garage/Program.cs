using Garage.Handlers;
using Garage.Models;
using Garage.UI;

namespace Garage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            GarageHandler? garageHandler = null;

            while (flag)
            {
                ConsoleUI.DisplayMenu();
                uint input = ConsoleUI.AskForUIntInput("Menu option: ");

                switch (input)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        uint size = ConsoleUI.AskForUIntInput("Enter garage's capacity: ");
                        garageHandler = new GarageHandler(size);
                        break;
                    case 2:
                        if(garageHandler == null)
                        {
                            ConsoleUI.DisplayWarningMessage("There is no garage, please create one first");
                            break;
                        }
                        else if (garageHandler.GarageFull())
                        {
                            ConsoleUI.DisplayWarningMessage("The garage is full");
                            break;
                        }
                        else
                        {
                            uint vehicle_type = ConsoleUI.AskVehicleType();
                            ConsoleUI.AskVehicleInfo(vehicle_type, garageHandler);
                            break;
                        }
                    case 3:
                        string reg_num = ConsoleUI.AskForRegNum("Enter license number, e.g. ABC444: ");
                        if (garageHandler == null)
                        {
                            ConsoleUI.DisplayWarningMessage("There is no garage, please create one first");
                            break;
                        }
                        if (garageHandler.RemoveVehicle(reg_num))
                        {
                            ConsoleUI.DisplaySuccessMessage("The vehicle removed successfully!");
                        }
                        else
                        {
                            ConsoleUI.DisplayWarningMessage("Remove of the vehicle failed");
                            break;
                        }
                        break;
                    case 4:
                        if (garageHandler == null)
                        {
                            ConsoleUI.DisplayWarningMessage("There is no garage, please create one first");
                            break;
                        }

                        ConsoleUI.DisplayAllVehicles(garageHandler);
                        break;

                    case 5:
                        if (garageHandler == null)
                        {
                            ConsoleUI.DisplayWarningMessage("There is no garage, please create one first");
                            break;
                        }

                        string search_input = ConsoleUI.AskUserForSearchVehicles();
                        var result = garageHandler?.SearchVehicles(search_input);

                        if (result != null)
                        {
                            ConsoleUI.DisplaySearchedVehicles(result);
                        }
                        else
                        {
                            ConsoleUI.DisplayWarningMessage("No vehicles found matching the search criteria");
                        }
                        break;
                }
            }
        }
    }
}
