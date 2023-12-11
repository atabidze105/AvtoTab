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
        //protected double _maxCapacity; //грузоподъемность (в тоннах)
        protected int _people; //кол-во людей
        protected int _peopleMax; //Макс. кол-во людей
        protected List<Int32> _coorStop;



        protected override void carCreation(string number, double fuelCapacity, string type)
        {
            base.carCreation(number, fuelCapacity, type);
            _people = 0;
            _peopleMax = 30;
            //_maxCapacity = 2.1;
        }

        protected void passengersGet() //Заход пассажиров
        {
            if (_people < _peopleMax)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nВведите число вошедших пассажиров:\n");
                        int people = Convert.ToInt32(Console.ReadLine());

                        if (people < 0)
                        {
                            Console.WriteLine("\nВведите число больше нуля. Попробуйте снова.\n");
                        }
                        else
                        {
                            if (_people + people > _peopleMax)
                            {
                                Console.WriteLine("\nНевозможно вместить столько пассажиров. Попробуйте снова.");
                            }
                            else
                            {
                                _people += people;
                                Console.WriteLine($"\nТекущее число пассажиров: {_people}.");
                                break;
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.");
                    }
                }
            }
        }

        protected void passengersLose() //Выход пассажиров
        {
            if (_people == 0)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nВведите число сошедших пассажиров:\n");
                        int people = Convert.ToInt32(Console.ReadLine());

                        if (people >= 0)
                        {
                            Console.WriteLine("\nНевозможно ввести отрицательное значение. Попробуйте снова.");
                        }
                        else
                        {
                            if (_people - people < 0)
                            {
                                Console.WriteLine("\nВведено значение, превышающее текущее число пассажиров. Попробуйте снова.");
                            }
                            else
                            {
                                if (people > 0)
                                {
                                    _people += people;
                                    Console.WriteLine($"\nТекущее число пассажиров: {_people}.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("\nНикто не покинул автобус.");
                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.");
                    }
                }
            }
        }


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
                        Way();
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