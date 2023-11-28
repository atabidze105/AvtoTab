using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoTab
{
    internal class Gruz:Avto
    {
        protected double _maxCapacity; //грузоподъемность
        protected int _people; //Кол-во людей

        protected override void carCreation(string number, double fuelCapacity, double fuelConsumption)
        {
            base.carCreation(number, fuelCapacity, fuelConsumption);
            _weight = 4;
            _people = 0;
            _maxCapacity = 2.1;
        }

    }
}
