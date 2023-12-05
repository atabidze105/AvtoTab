

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


        protected override void carCreation(string number, double fuelCapacity, string type)
        {
            base.carCreation(number, fuelCapacity, type);
            _maxCapacity = 2;
        }

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
                    distancePlanning(avtos);

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
                    
                    if (_weightCargo > 0.1 && _weightCargo <= 1)
                    {
                        _speed *= 0.6;
                        Console.WriteLine($"\nПредупреждение! С текущим весом груза в {_weightCargo} т скорость уменьшена на 40% - {_speed} км/ч.") ;
                    }
                    else
                    {
                        if (_weightCargo > 1 && _weightCargo <= 2)
                        {
                            _speed *= 0.2;
                            Console.WriteLine($"\nПредупреждение! С текущим весом груза в {_weightCargo} т скорость уменьшена на 80% - {_speed} км/ч.");
                        }
                    }
                    if (_speed > 0 && _speed <= 180)
                    {
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
            double fuelDistance = _currentFuel / (_fuelConsumption / 100); //Расстояние, которое может проехать машина с заправленным баком
            Console.WriteLine($"\nНеобходимо проехать {distance} км.\n\nНачало поездки.");
            distance -= fuelDistance;

            while (distance > 0) //Цикл езды
            {
                _speed = 0;
                _currentFuel = 0; //обнуление кол-ва топлива
                _milleage += fuelDistance; //Увеличение пробега

                Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nВес груза: {_weightCargo} т.\nОсталось ехать {Math.Round(distance, 2)} км.\nТребуется дозаправка.");
                FillFuel(); //Обращение к методу заправки
                speedUp();
                fuelDistance = _currentFuel / (_fuelConsumption / 100); //Обновление расстояния, котрое может проехать машина с заправленным на текущее кол-во топлива баком
                distance -= fuelDistance; //Обновление расстояния, которое необходимо проехать
            }

            _speed = 0;
            _fuelConsumption = 0;
            _milleage += (fuelDistance += distance);//По завершении цикла расстояние становится отрицательным значением. Здесь остаток расстояния складывается с расстоянием,которая может проехать машина, после чего обновляется пробег
            _currentFuel -= (fuelDistance * (_fuelConsumption / 100)); //Определение остатка топлива

            Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nВес груза: {_weightCargo} т.");
        }

        protected override void Drive(List<Avto> avtos) //Поездка для грузовика
        {
            while (Math.Floor(_currentFuel) == 0) //Если у машины нет топлива, произойдет обращение к методу заправки
            {
                Console.WriteLine("\nНевозможно начать поездку с пустым бензобаком.\nТребуется дозаправка");
                FillFuel();
            }

            getDistance(avtos);
            speedUp();

            subDrive(_distance);

            Console.WriteLine("\nМашина прибыла в точку погрузки.");
            load();

            subDrive(_distanceToUnload);

            Console.WriteLine("\nМашина прибыла в точку разгрузки.\nВозврат на базу.");
            _weightCargo = 0;
            subDrive(_distanceBack);

            Console.WriteLine("\nМашина прибыла на базу.\n\nПоездка завершена.");
        }
    }
}