using Garage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Garage
{
    internal class Garage<T>: IEnumerable<T> where T : Vehicle
    {
        private T[] vehicles;
        private uint capacity;
        private uint count = 0;

        public Garage(uint capacity)
        {
            this.capacity = capacity;
            vehicles = new T[capacity];
        }

        public bool AddVehicle(T vehicle)
        {
            for(int i = 0; i < capacity; i++)
            {
                if(vehicles[i] == null)
                {
                    vehicles[i] = vehicle;
                    count++;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveVehicle(string register_plate_number)
        {
            for(int i = 0; i < count; i++)
            {
                if (vehicles[i].LicensePlateNumber().ToLower() == register_plate_number.ToLower())
                {
                    vehicles[i] = default;
                    count--;
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var vehicle in vehicles)
            {
                if(vehicle != null)
                    yield return vehicle;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
