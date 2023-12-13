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
        protected int _people; //кол-во людей
        protected int _peopleMax; //Макс. кол-во людей
        protected List<string> _coorStop = new();


        protected override void carCreation(string number, double fuelCapacity, string type)
        {
            base.carCreation(number, fuelCapacity, type);
            _people = 0;
            _peopleMax = 30;
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

        protected void stopPlannnimng()
        {
            while (true)
            {
                try
                {
                    if (_coorStop.Count < 2)
                    {
                        Console.WriteLine("\nНеобходимо добавить хотя бы две остановки.");
                    }

                    stopAdd();

                    if (_coorStop.Count > 2)
                    {
                        string answer = "";
                        while (answer == "")
                        {
                            Console.WriteLine($"\nТекущее количество остановок: {_coorStop.Count}.\nХотите добавить еще остановку? (да/нет)\n");
                            answer = Console.ReadLine();
                            switch (answer)
                            {
                                case "да":
                                case "нет":
                                    break;
                                default:
                                    answer = "";
                                    break;
                            }
                        }
                        if (answer == "нет")
                        {
                            plan();
                            answer = "";
                            while (answer == "")
                            {
                                Console.WriteLine($"\nВы уверены, что ввели все координаты остановок корректно и хотите продолжить? (да/нет)\n");
                                answer = Console.ReadLine();
                                switch (answer)
                                {
                                    case "да":
                                    case "нет":
                                        break;
                                    default:
                                        answer = "";
                                        break;
                                }
                            }
                            if (answer == "да")
                            {
                                break;
                            }
                            else
                            {
                                _coorStop.Clear();
                                Console.WriteLine("\nВыполнен сброс списка остановок. Начните снова с самого начала");
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("\nКакое-то значение ведено некорректно. Попробуйте снова.");

                }
            }
        }

        protected void plan()
        {
            Console.WriteLine($"Автобус {_number} проедет по следующему маршруту (x;y):");
            int i = 1;
            foreach (string coor in _coorStop)
            {
                Console.WriteLine($"{i}. {coor};");
                i++;
            }
            Console.WriteLine();
        }

        protected void stopAdd() //Добавление новой остановки в список остановок
        {
            while(true)
            {
                try
                {
                    Console.WriteLine("\nДобавьте координату остановки.\nВведите X:\n");
                    double stopX = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nВведите Y:\n");
                    double stopY = Convert.ToDouble(Console.ReadLine());

                    string stopCoor = Convert.ToString(stopX) + ";" + Convert.ToString(stopY);

                    foreach (string coor in _coorStop)
                    {
                        if (stopCoor != coor) //Изменить условие СРАВНИТЬ ТЕКУЩУЮ КООРДИНАТУ С ПРЕДЫДУЩЕЙ   
                        {
                            _coorStop.Add(coor);
                        }
                        else
                        {
                            Console.WriteLine("\n");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.");
                }
            }
        }
        


        protected override void commandCenter(List<Avto> avtos)
        {
            Console.WriteLine($"\n\nДобро пожаловать на панель управления машины {_number}.\n");
            string continuation = "";
            while (continuation == "")
            {
                Console.WriteLine(" Чтобы узнать информацию о машине, выберите \"1\".\n Чтобы запланировать маршрут, выберите \"2\".\n Чтобы заправить машину, выберите \"3\".\n Чтобы начать поездку, выберите \"4\".\n\n");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("\n");
                        DisplayInfo();
                        break;
                    case "2":
                        stopPlannnimng();
                        break;
                    case "3":
                        FillFuel();
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