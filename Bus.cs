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
        protected double _maxCapacity; //грузоподъемность (в тоннах)
        protected int _people; //Кол-во людей

        protected override void carCreation(string number, double fuelCapacity, string type)
        {
            base.carCreation(number, fuelCapacity, type);
            _people = 0;
            _maxCapacity = 2.1;
        }


    }
}