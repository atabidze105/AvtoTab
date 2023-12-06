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
        protected List<Int32> _coorStop;

        protected override void carCreation(string number, double fuelCapacity, string type)
        {
            base.carCreation(number, fuelCapacity, type);
            _people = 0;
            _maxCapacity = 2.1;
        }

        protected void passengers() //Заход пассажиров
        {

        }

        protected override void commandCenter(List<Avto> avtos)
        {
            Console.WriteLine($"\n\nДобро пожаловать на панель управления машины {_number}.\n");
            string continuation = "";
            while (continuation == "")
            {
                Console.WriteLine(" Чтобы узнать информацию о машине, выберите \"1\".\n Чтобы заправить машину, выберите \"2\".\n Чтобы запланировать маршрут, выберите \"3\".\n Чтобы начать поездку, выберите \"4\".\n\n");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("\n");
                        DisplayInfo();
                        break;
                    case "2":
                        FillFuel();
                        break;
                    case "3":
                        //планирование маршрута
                        break;
                    case "4":
                        Drive(avtos);
                        break;
                    default:
                        Console.WriteLine("\nКоманды с таким номером не существует.");
                        break;
                }
                Console.WriteLine("\nЧтобы продолжить нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".\n");
                continuation = Console.ReadLine();
            }
        }

    }
}