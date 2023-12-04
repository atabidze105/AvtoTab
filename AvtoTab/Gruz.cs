using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvtoTab
{
    internal class Gruz : Avto
    {
        protected double _maxCapacity; //грузоподъемность
        protected int _weight; //Вес
        protected double _weightCargo; //Вес груза


        protected override void carCreation(string number, double fuelCapacity, string type)
        {
            base.carCreation(number, fuelCapacity, type);
            _weight = 4;
            _maxCapacity = 2;
        }

    }
}