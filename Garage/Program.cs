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
            GarageHandler garageHandler;

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
                        ConsoleUI.AskVehicleType();
                        break;
                }
            }
        }
    }
}
