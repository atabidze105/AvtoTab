

namespace AvtoTab
{
    internal class Gruz : Avto
    {
        protected double _maxCapacity; //грузоподъемность
        protected double _weightCargo; //Вес груза
        protected double _distanceToUnload; //расстояние от места погрузки до места рагрузки (_distance - от начальных координат до места погрузки)
        protected double _distanceBack; //расстояние от места разгрузки к начальным координатам
        protected double _deliveryX; //координаты места разгрузки
        protected double _deliveryY;
        protected List<List<string>> _coordinatesAll = new(); //Координаты всего пути



        protected override void getDistance(List<Avto> avtos) //Грузовик останавливается в трех местах. всего 4 точки, после второй остановки возвращается к начальным координатам
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nНеобходимо ввести координаты базы.\nВведите \"x\":\n");
                    _startX = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nВведите \"y\":\n");
                    _startY = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nНеобходимо ввести координаты точки погрузки.\nВведите \"x\":\n");
                    _endX = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nВведите \"y\":\n");
                    _endY = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nНеобходимо ввести координаты точки разгрузки.\nВведите \"x\":\n");
                    _deliveryX = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nВведите \"y\":\n");
                    _deliveryY = Convert.ToDouble(Console.ReadLine());

                    _distance = Math.Sqrt(Math.Pow(_endX - _startX, 2) + Math.Pow(_endY - _startY, 2)); //вычисление расстояния к точке погрузки
                    _distanceToUnload = Math.Sqrt(Math.Pow(_deliveryX - _endX, 2) + Math.Pow(_deliveryY - _endY, 2)); //вычисление расстояния к точке разгрузки
                    _distanceBack = Math.Sqrt(Math.Pow(_startX - _deliveryX, 2) + Math.Pow(_startY - _deliveryY, 2)); //вычисление расстояния к точке погрузки

                    Console.WriteLine();
                    coorPlanning(_startX, _startY, _endX, _endY);
                    coorPlanning(_endX, _endY, _deliveryX, _deliveryY);
                    coorPlanning(_deliveryX, _deliveryY, _startX, _startY);
                    fullDistance();


                    string answer = "";
                    while (answer == "")
                    {
                        Console.WriteLine("\nВы уверены, что ввели все правильно и хотите продолжить? (да/нет)\n");
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
                }
                catch
                {
                    Console.WriteLine("\nКоординаты введены некорректно. Попробуйте снова с самого начала.");
                }
            }
        }

        protected void coorPlanning(double x1, double y1, double x2, double y2)
        {
            double startSample = x1;
            List<string> coordinates = new();


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

            _coordinatesAll.Add(coordinates);
        }

        protected void fullDistance()
        {
            int i = 0;
            foreach (List<string> coorDistance in _coordinatesAll)
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

        protected void load()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите груз (в т), который повезет машина (до 2 т):\n");
                    double cargo = Convert.ToDouble(Console.ReadLine());
                    if (cargo > 0 && cargo <= 2)
                    {
                        _weightCargo = cargo;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nВведено значение вне заданного диапазона. Попробуйте снова.\n");
                    }
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.\n");
                }
            }
        }

        protected override void speedUp()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nВведите значение скорости (от 1 до 180 км/ч), до которого хотите разогнаться:\n");
                    _speed = Convert.ToDouble(Console.ReadLine());

                    _speed = _weightCargo > 0.1 && _weightCargo <= 1 ? _speed * 0.6 : (_weightCargo > 1 && _weightCargo <= 2 ? _speed * 0.2 : _speed);
                    string persent = _weightCargo > 0.1 && _weightCargo <= 1 ? "40" : (_weightCargo > 1 && _weightCargo <= 2 ? "80" : "");

                    if (_speed > 0 && _speed <= 180)
                    {
                        if (_weightCargo > 0.1 && _weightCargo <= 2)
                        {
                            Console.WriteLine($"\nПредупреждение! С текущим весом груза в {_weightCargo} т скорость уменьшена на {persent}% - {_speed} км/ч.");
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

        protected void subDrive(double distance)
        {
            if (Math.Floor(_speed) == 0) //Если показатель скорости у машины равен 0, то будет инициирован разгон
            {
                speedUp();
            }
            double fuelDistance = _currentFuel / (_fuelConsumption / 100); //Расстояние, которое может проехать машина с заправленным баком
            Console.WriteLine($"\nНеобходимо проехать {distance} км.\n\nНачало поездки.");
            double needFuel = distance * (_fuelConsumption / 100); //Требуемое кол-во топлива для преодоления заданного расстояния
            distance -= fuelDistance;
            _currentFuel = fuelDistance > distance ? _currentFuel - needFuel : _currentFuel; //Если расстояние, которое может проехать машина с заправленным баком больше, чем то, которое нужно проехать, то от текущего кол-ва топ-ва отнимается требуемое для преодоления заданного расст-я

            while (distance > 0) //Цикл езды
            {
                _speed = 0;
                _currentFuel = 0; //обнуление кол-ва топлива
                _milleage += fuelDistance; //Увеличение пробега

                Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nРасход топлива: {_fuelConsumption} л на 100 км.\nВес груза: {_weightCargo} т.\nОсталось ехать {Math.Round(distance, 2)} км.\nТребуется дозаправка.");
                FillFuel(); //Обращение к методу заправки
                speedUp();
                fuelDistance = _currentFuel / (_fuelConsumption / 100); //Обновление расстояния, котрое может проехать машина с заправленным на текущее кол-во топлива баком
                distance -= fuelDistance; //Обновление расстояния, которое необходимо проехать
                needFuel = _currentFuel + (distance * (_fuelConsumption / 100));
                _currentFuel = fuelDistance > distance ? _currentFuel - needFuel : _currentFuel;
            }

            _speed = 0;
            _milleage += (fuelDistance += distance);//По завершении цикла расстояние становится отрицательным значением. Здесь остаток расстояния складывается с расстоянием,которая может проехать машина, после чего обновляется пробег
            Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nРасход топлива: {_fuelConsumption} л на 100 км.\nВес груза: {_weightCargo} т.");
            _fuelConsumption = 0;
        }

        protected override void Drive(List<Avto> avtos) //Поездка для грузовика
        {
            while (_distance < 0)
            {
                Console.WriteLine("\nНеобходимо запланировать маршрут");
                getDistance(avtos);
            }
            while (Math.Floor(_currentFuel) == 0) //Если у машины нет топлива, произойдет обращение к методу заправки
            {
                Console.WriteLine("\nНевозможно начать поездку с пустым бензобаком.\nТребуется дозаправка");
                FillFuel();
            }

            subDrive(_distance);

            Console.WriteLine("\nМашина прибыла в точку погрузки.");
            load();

            subDrive(_distanceToUnload);

            Console.WriteLine("\nМашина прибыла в точку разгрузки.\nВозврат на базу.");
            _weightCargo = 0;
            subDrive(_distanceBack);

            Console.WriteLine("\nМашина прибыла на базу.\n\nПоездка завершена.");
            _coordinates.Clear();
        }

        protected virtual void commandCenter(List<Avto> avtos) //Главный метод
        {
            Console.WriteLine($"\n\nДобро пожаловать на панель управления машины {_number}.\n");
            string continuation = "";
            while (continuation == "")
            {
                Console.WriteLine(" Чтобы узнать информацию о машине, выберите \"1\".\n Чтобы запланировать маршрут, выберите \"2\".\n Чтобы проверить маршрут на возможность попадания в аварию, нажмите \"3\".\n Чтобы заправить машину, выберите \"4\".\n Чтобы начать поездку, выберите \"5\".\n\n");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("\n");
                        DisplayInfo();
                        break;
                    case "2":
                        getDistance(avtos);
                        break;
                    case "3":
                        if (_coordinates.Count == 0)
                        {
                            Console.WriteLine("\nЧтобы проверить маршрут, необходимо его запланировать.");
                            getDistance(avtos);
                            distancePlanning(avtos); //Проверка на наличие потенциальных столкновений с другими машинами в точках координат заданного пути
                        }
                        else
                        {
                            distancePlanning(avtos); //Проверка на наличие потенциальных столкновений с другими машинами в точках координат заданного пути
                        }
                        break;
                    case "4":
                        FillFuel();
                        break;
                    case "5":
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