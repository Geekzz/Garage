using Garage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Garage
{
    internal class Garage<T>(uint capacity) : IEnumerable<T> where T : Vehicle
    {
        // Readonly = makes sure it cannot be changed with method
        // Reference: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
        private readonly T[] vehicles = new T[capacity];
        private readonly uint capacity = capacity;
        private uint count = 0; // Count = how many vehicles are in garage, so readonly isnt needed here

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
            for(int i = 0; i < capacity; i++)
            {
                if (vehicles[i] != null)
                {
                    if (vehicles[i].LicensePlateNumber().ToLower() == register_plate_number.ToLower())
                    {
                        vehicles[i] = default;
                        count--;
                        return true;
                    }
                }
            }

            return false;
        }

        public uint GetCapacity()
        {
            return this.capacity;
        }

        public bool IsGarageFull()
        {
            return count >= capacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var vehicle in vehicles)
            {
                // Skipping checking if vehicle is null because its nicer to display all available park seats
                //if(vehicle != null)
                yield return vehicle;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
