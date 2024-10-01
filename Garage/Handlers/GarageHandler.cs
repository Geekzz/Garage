using Garage.Garage;
using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Handlers
{
    internal class GarageHandler<T> where T : Vehicle
    {
        private Garage<T> garage;

        public GarageHandler(int capacity)
        {
            // ny garage med dess storlek
            garage = new Garage<T>(capacity);
        }

    }
}
