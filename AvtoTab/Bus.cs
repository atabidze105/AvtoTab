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
        protected double _peopleWeight; //Общий вес людей в автобусе
        protected List<string> _coorStop = new(); //Список координат остановок
        protected List<List<string>> _coorWays = new(); //Список списков координат пути от одной остановки до следующей

        protected override void carCreation(string number, double fuelCapacity, string type)
        {
            base.carCreation(number, fuelCapacity, type);
            _people = 0;
            _peopleWeight = 0;
        }

        protected void weight()
        {
            _peopleWeight = 0.07 * _people;
        }

        protected void passengersGet() //Заход пассажиров
        {
            if (_people < 30)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nВведите число вошедших пассажиров:\n");
                        int people = Convert.ToInt32(Console.ReadLine());

                        if (people > 0)
                        {
                            if (_people + people > 30)
                            {
                                Console.WriteLine("\nНевозможно вместить столько пассажиров. Попробуйте снова.");
                            }
                            else
                            {
                                _people += people;
                                Console.WriteLine($"\nТекущее число пассажиров: {_people}.");
                                weight();
                                break;
                            }
                        }
                        else
                        {
                            if (people < 0)
                            {
                                Console.WriteLine("\nВведите число больше нуля. Попробуйте снова.\n");
                            }
                            else
                            {
                                Console.WriteLine("\nНикто не покинул автобус.");
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
                                    weight();
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

        protected void fuelConsumption(double speed) //рассчет расхода топлива от скорости
        {
            _fuelConsumption = speed >= 0 && speed <= 45 ? 12 : (speed > 45 && speed <= 100 ? 9 : 12.5);
        }

        protected override void speedUp() 
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nВведите значение скорости (от 1 до 180 км/ч), до которого хотите разогнаться:\n");
                    _speed = Convert.ToDouble(Console.ReadLine());

                    _speed = _peopleWeight > 0.1 && _peopleWeight <= 1 ? _speed * 0.6 : (_peopleWeight > 1 && _peopleWeight <= 2.1 ? _speed * 0.2 : _speed);
                    string persent = _peopleWeight > 0.1 && _peopleWeight <= 1 ? "40" : (_peopleWeight > 1 && _peopleWeight <= 2.1 ? "80" : "");

                    if (_speed > 0 && _speed <= 180)
                    {
                        if (_peopleWeight > 0.1 && _peopleWeight <= 2.1)
                        {
                            Console.WriteLine($"\nПредупреждение! С текущим весом груза в {_peopleWeight} т скорость уменьшена на {persent}% - {_speed} км/ч.");
                        }
                        fuelConsumption(_speed);
                        break; //Выход из цикла
                    }
                    else
                    {
                        Console.WriteLine("\nВведено значение вне заданного диапазона. Попробуйте снова.");
                    }
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.");
                }
            }
        }

        protected void k()
        {

        }


        protected void stopPlannnimng() //Планирование маршрута
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

                    if (_coorStop.Count > 1)
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
                            for (int i = _coorStop.Count - 2 ; i > -1 ; i--) //Дополнение массива координат (возврат на базу) 
                            {
                                _coorStop.Add(_coorStop[i]);
                            }

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

        protected void plan() //Отображение списка остановок, которые проедет автобус
        {
            Console.WriteLine($"Автобус {_number} проедет по следующему маршруту (x;y):\n");
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
            while (true)
            {
                try
                {
                    Console.WriteLine("\nДобавьте координату остановки.\nВведите X:\n");
                    double stopX = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nВведите Y:\n");
                    double stopY = Convert.ToDouble(Console.ReadLine());

                    string stopCoor = Convert.ToString(stopX) + ";" + Convert.ToString(stopY);

                    if (_coorStop.Count >= 2)
                    {
                        if (stopCoor != _coorStop.Last())
                        {
                            Console.WriteLine("\nДобавлена коордиината остановки.");
                            _coorStop.Add(stopCoor);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nНевозможно добавить остановку на координате, идентичной предыдущей. Попробуйте снова.");
                        }
                    }
                    else
                    {
                        if (_coorStop.Count != 0)
                        {
                            if (stopCoor != _coorStop[0])
                            {
                                Console.WriteLine("\nДобавлена коордиината остановки.");
                                _coorStop.Add(stopCoor);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nНевозможно добавить остановку на координате, идентичной предыдущей. Попробуйте снова.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nДобавлена коордиината остановки.");
                            _coorStop.Add(stopCoor);
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

        protected override void Drive(List<Avto> avtos) //Переписать
        {
            while (_coordinates.Count == 0) //Если маршрут не спланирован, будет вызван соответствующий метод
            {
                Console.WriteLine("\nНеобходимо запланировать маршрут");
                stopPlannnimng();
            }

            while (Math.Floor(_currentFuel) == 0) //Если у машины нет топлива, произойдет обращение к методу заправки
            {
                Console.WriteLine("\nНевозможно начать поездку с пустым бензобаком.\nТребуется дозаправка");
                FillFuel();
            }

            speedUp();

            double fuelDistance = _currentFuel / (_fuelConsumption / 100); //Расстояние, которое может проехать машина с заправленным баком
            Console.WriteLine($"\nНеобходимо проехать {_distance} км.\n\nНачало поездки.");
            _distance -= fuelDistance;

            while (_distance > 0) //Цикл езды
            {
                _speed = 0;
                _currentFuel = 0; //обнуление кол-ва топлива
                _milleage += fuelDistance; //Увеличение пробега

                Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nОсталось ехать {Math.Round(_distance, 2)} км.\nТребуется дозаправка.");
                FillFuel(); //Обращение к методу заправки
                speedUp();
                fuelDistance = _currentFuel / (_fuelConsumption / 100); //Обновление расстояния, котрое может проехать машина с заправленным на текущее кол-во топлива баком
                _distance -= fuelDistance; //Обновление расстояния, которое необходимо проехать
            }

            _speed = 0;
            _milleage += (fuelDistance += _distance);//По завершении цикла расстояние становится отрицательным значением. Здесь остаток расстояния складывается с расстоянием,которая может проехать машина, после чего обновляется пробег
            _currentFuel -= (fuelDistance * (_fuelConsumption / 100)); //Определение остатка топлива

            Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\n\nПоездка завершена.");
            _coordinates.Clear();
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