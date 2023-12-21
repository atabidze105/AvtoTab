using System;
using System.Collections.Generic;
using System.Drawing;
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
        protected double _baseX; //Координаты автобусной стоянки 
        protected double _baseY;
        protected List<string> _coorStop = new(); //Список координат остановок
        protected List<double> _distances = new();
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
            if (_people <= 30)
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
                                Console.WriteLine("\nНевозможно ввести отрицательное значение. Попробуйте снова.\n");
                            }
                            else
                            {
                                Console.WriteLine("\nНикто не зашел в автобус.");
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
            if (_people != 0)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nВведите число сошедших пассажиров:\n");
                        int people = Convert.ToInt32(Console.ReadLine());

                        if (people <= 0)
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
                                    _people -= people;
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
                            Console.WriteLine($"\nПредупреждение! С текущим количеством пассажиров ({_people} чел.) с общим весом в {Math.Round(_peopleWeight, 2)} т скорость уменьшена на {persent}% - {_speed} км/ч.");
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

        protected void busBase() //Добавление автобусной стоянки
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Необходимо добавить координаты базы.\nВведите X:\n");
                    _baseX = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nВведите Y:\n");
                    _baseY = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nКоординаты базы заданы.");
                    break;
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение. Попробуйте снова с самого начала.");
                }
            }
        }

        protected void listClear()
        {
            _coorStop.Clear();
            _distances.Clear();
            _coorWays.Clear();
            _coordinates.Clear();
        }

        protected void stopPlanning(List<Avto> avtos) //Планирование маршрута
        {
            while (true)
            {
                try
                {
                    if (_coorStop.Count < 2)
                    {
                        Console.WriteLine($"\nТекущее количество остановок: {_coorStop.Count}.\nНеобходимо добавить хотя бы две остановки.");;
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
                            for (int i = _coorStop.Count - 2; i > -1; i--) //Дополнение массива координат (возврат на базу) 
                            {
                                _coorStop.Add(_coorStop[i]);
                            }
                            getDistances();
                            plan();
                            distancePlanning(avtos);
                            fullDistance();

                            answer = "";
                            while (answer == "")
                            {
                                Console.WriteLine($"Вы уверены, что ввели все координаты остановок корректно и хотите продолжить? (да/нет)\n");
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
                                listClear();
                                Console.WriteLine("\nВыполнен сброс списка остановок. Начните снова с самого начала.");
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("\nКакое-то значение введено некорректно. Попробуйте снова.");
                }
            }
        }

        protected void plan() //Отображение списка остановок, которые проедет автобус
        {
            Console.WriteLine($"\nАвтобус {_number} проедет по следующему маршруту (x;y):\n");
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

                    if (stopCoor != (Convert.ToString(_baseX) + ";" + Convert.ToString(_baseY)))
                    {
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
                    else
                    {
                        Console.WriteLine("\nНевозможно задать координаты базы как остановку.");
                    }
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.");
                }
            }
        }

        protected void getCoorArray(double x1, double y1, double x2, double y2) //Универсальный метод вычисления расстояния, а также построения маршрута по координатам. На выходе из метода в списки расстояний и пуей добавляются соответственно расстояние и список координат
        {
            double startSample = x1;
            List<string> coordinates = new();

            double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)); //вычисление расстояния
            _distances.Add(distance);

            if (x1 != x2) //Если начальный Х не равен конечному Х, то за основу построения маршрута берется Х. У находится по формуле прямой через две точки на пл-ти
            {
                if (x1 < x2)//Условия для направления движения
                {
                    while (startSample <= x2)
                    {
                        coordinates.Add(Convert.ToString(startSample));
                        startSample++;
                    }
                }
                else
                {
                    while (startSample >= x2)
                    {
                        coordinates.Add(Convert.ToString(startSample));
                        startSample--;
                    }
                }
                for (int i = 0; i < coordinates.Count; i++)
                {
                    coordinates[i] += $"; {Convert.ToString(y1 + ((y2 - y1) * (Convert.ToDouble(coordinates[i]) - x1) / (x2 - x1)))}"; //ко всем элементам списка добавляется сооттветствующая координата Y. которая высчитывается по формуле прямой через две точки на плоскости
                }
            }
            else
            {
                if (y1 < y2)
                {
                    while (y1 <= y2)
                    {
                        coordinates.Add($"{Convert.ToString(x1)};{Convert.ToString(y1)}");
                        y1++;
                    }
                }
                else
                {
                    while (y1 >= y2)
                    {
                        coordinates.Add($"{Convert.ToString(x1)};{Convert.ToString(y1)}");
                        y1--;
                    }
                }
            }

            _coorWays.Add(coordinates); //Добавление готового списка в список путей
        }

        protected void getDistances() //Получение расстояний между остановками и путей в координатах
        {
            string[] firstStop = _coorStop[0].Split(';');
            getCoorArray(_baseX, _baseY, Convert.ToDouble(firstStop[0]), Convert.ToDouble(firstStop[1]));

            for (int i = 0; i < _coorStop.Count - 1; i++)
            {
                string[] fromStop = _coorStop[i].Split(";");
                string[] toStop = _coorStop[i + 1].Split(";");
                getCoorArray(Convert.ToDouble(fromStop[0]), Convert.ToDouble(fromStop[1]), Convert.ToDouble(toStop[0]), Convert.ToDouble(toStop[1]));
            }

            string[] lastStop = _coorStop[_coorStop.Count - 1].Split(";");
            getCoorArray(Convert.ToDouble(lastStop[0]), Convert.ToDouble(lastStop[1]), _baseX, _baseY);
        }

        protected string coorSearch(string coor, List<string> coorArray)
        {
            foreach (string coorA in coorArray)
            {
                if (coor == coorA)
                {
                    return coor;
                }
            }
            return null;
        }

        protected void fullDistance()
        {
            int i = 0;
            List<List<string>> coorWaysHalf = new();
            for (int j = 0; j <= Math.Floor(Convert.ToDouble(_coorStop.Count) / 2); j++)
            {
                coorWaysHalf.Add(_coorWays[j]);
            }

            foreach (List<string> coorDistance in coorWaysHalf)
            {
                foreach (string coor in coorDistance)
                {
                    if (_coordinates.Count == 0)
                    {
                        _coordinates.Add(coor);
                    }
                    else
                    {
                        if (coor != _coordinates[i])
                        {
                            if (coorSearch(coor, _coordinates) == null)
                            {
                                _coordinates.Add(coor);
                                i++;
                            }
                        }
                    }
                }
            }
        }

        protected void subDrive(double distance, int stop) //Поездка
        {
            if (Math.Floor(_speed) == 0) //Если показатель скорости у машины равен 0, то будет инициирован разгон
            {
                speedUp();
            }
            double fuelDistance = _currentFuel / (_fuelConsumption / 100); //Расстояние, которое может проехать машина с заправленным баком
            Console.WriteLine($"Необходимо проехать {Math.Round(distance, 2)} км.\n\nНачало поездки.");
            double needFuel = distance * (_fuelConsumption / 100); //Требуемое кол-во топлива для преодоления заданного расстояния
            distance -= fuelDistance;
            _currentFuel = fuelDistance > distance ? _currentFuel - needFuel : _currentFuel; //Если расстояние, которое может проехать машина с заправленным баком больше, чем то, которое нужно проехать, то от текущего кол-ва топ-ва отнимается требуемое для преодоления заданного расст-я

            while (distance > 0) //Цикл езды
            {
                _speed = 0;
                _currentFuel = 0; //обнуление кол-ва топлива
                _milleage += fuelDistance; //Увеличение пробега

                Console.WriteLine($"\nАвтобус проехал {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nПассажиров в салоне: {_people} чел..\nОсталось ехать {Math.Round(distance, 2)} км.\nТребуется дозаправка.");
                FillFuel(); //Обращение к методу заправки
                speedUp();
                fuelDistance = _currentFuel / (_fuelConsumption / 100); //Обновление расстояния, котрое может проехать машина с заправленным на текущее кол-во топлива баком
                distance -= fuelDistance; //Обновление расстояния, которое необходимо проехать
                needFuel = _currentFuel + (distance * (_fuelConsumption / 100));
                _currentFuel = fuelDistance > distance ? _currentFuel - needFuel : _currentFuel;
            }

            _speed = 0;
            _fuelConsumption = 0;
            _milleage += (fuelDistance += distance);//По завершении цикла расстояние становится отрицательным значением. Здесь остаток расстояния складывается с расстоянием,которая может проехать машина, после чего обновляется пробег
            _currentFuel -= (fuelDistance * (_fuelConsumption / 100)); //Определение остатка топлива
            Console.WriteLine($"\nПройдено {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nПассажиров в салоне: {_people} чел..");
        }


        protected override void Drive(List<Avto> avtos) //Вся поездка автобуса по прописанному маршруту
        {
            while (_distances.Count == 0) //Если маршрут не спланирован, будет вызван соответствующий метод
            {
                Console.WriteLine("\nНеобходимо запланировать маршрут");
                busBase();
                stopPlanning(avtos);
            }

            while (Math.Floor(_currentFuel) == 0) //Если у машины нет топлива, произойдет обращение к методу заправки
            {
                Console.WriteLine("\nНевозможно начать поездку с пустым бензобаком.\nТребуется дозаправка");
                FillFuel();
            }

            Console.WriteLine($"Автобус начал движение по заданному маршруту. Для достижения первой остановки осталось проехать {Math.Round(_distances[0],2)} км."); ;

            for (int i = 0; i < _distances.Count - 2; i++)
            {
                Console.WriteLine($"\nНачато движение к остановке {i+1} ({_coorStop[i]}). ");
                subDrive(_distances[i], i);
                Console.WriteLine($"\nАвтобус прибыл на остановку {i+1} ({_coorStop[i]}).");
                //if (i > 0)
                //{
                    passengersLose();
                //}
                passengersGet();
                Console.WriteLine($"\nСледующая остановка: {i + 2} ({_coorStop[i + 1]}).");
            }

            Console.WriteLine($"\nНачато движение к остановке {_distances.Count - 2} ({_coorStop[_distances.Count - 2]}). ");
            subDrive(_distances[_distances.Count - 2], _distances.Count - 2);

            Console.WriteLine("\nАвтобус прибыл на конечную остановку. Пассажиры покидают салон.");
            _people = 0;
            Console.WriteLine($"\nАвтобус возвращается на базу.");

            subDrive(_distances[_distances.Count - 1], _distances.Count - 1);

            Console.WriteLine("\nАвтобус вернулся на базу.");
            listClear();
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
                        busBase();
                        stopPlanning(avtos);
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