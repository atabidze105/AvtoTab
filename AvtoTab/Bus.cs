using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvtoTab
{
    internal class Bus : Avto
    {
        protected int _people;

        protected override void carCreation(string number, double fuelCapacity, double fuelConsumption)
        {
            base.carCreation( number,  fuelCapacity,  fuelConsumption);
            _weight = 3;
            _people = 0;
        }


    }
}
