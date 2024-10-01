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
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        flag = false;
                        break;
                    case "1":
                        ConsoleUI.AskGarageSize();
                        uint size = uint.Parse(Console.ReadLine());
                        garageHandler = new GarageHandler(size);
                        break;
                    case "2":
                        ConsoleUI.AskVehicleType();
                        break;
                }
            }
        }
    }
}
