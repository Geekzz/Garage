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
        private int capacity;
        private int count = 0;

        public Garage(int capacity)
        {
            this.capacity = capacity;
            vehicles = new T[capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0; i < count; i++)
            {
                yield return vehicles[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
