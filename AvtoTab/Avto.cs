
namespace AvtoTab
{
    internal class Avto
    {
        protected string _number; //Номер машины
        protected string _type; //Тип машины
        protected double _fuelCapacity; //Макс кол-во бензина в баке
        protected double _fuelConsumption; //Расход топлива
        protected double _currentFuel; //Текущее кол-во бензина
        protected double _startX; //Поля для координат (начальные и конечные)
        protected double _startY;
        protected double _endX;
        protected double _endY;
        protected double _distance;//расстояние
        protected double _milleage;//Пробег
        protected double _speed; //Скорость
        protected double _maxSpeed; //Максимальная скорость
        protected List<string> _coordinates = new(); //Список координат, по которым проезжает машина

        private void carStart(List<Avto> avtos)
        {
            Console.WriteLine("Добро пожаловать в Машины 2. Для начала работы необходимо создать хотя бы одну машину.");
            while (true)
            {
                try
                {
                    string answerType = "";
                    while (answerType == "")
                    {
                        Console.WriteLine("\nВыберите тип машины, которую Вы хотите создать:\n 1 - легковой автомобиль;\n 2 - автобус;\n 3 - грузовой автомобиль.\n");
                        answerType = Console.ReadLine();
                        switch (answerType)
                        {
                            case "1":
                            case "2":
                            case "3":
                                break;
                            default:
                                Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.\n");
                                answerType = "";
                                break;
                        }
                    }

                    Console.WriteLine("\nВведите номер машины:");
                    string avtoNumber = Console.ReadLine();
                    double avtoFCapacity;
                    if (answerType == "1")
                    {
                        Console.WriteLine("Введите вместимость бензобака (в литрах):");
                        avtoFCapacity = Convert.ToDouble(Console.ReadLine());
                    }
                    else
                    {
                        avtoFCapacity = answerType == "2" ? 60 : 80;
                    }


                    foreach (Avto avto in avtos) //Проверка на уникальность номера
                    {
                        if (avtoNumber == avto._number)
                        {
                            avtoNumber = "";
                            Console.WriteLine("\nВведенный номер занят.");
                            break;
                        }
                    }
                    if (avtoNumber == "" || avtoFCapacity <= 0) //Условие не позволяет создать аккаунт с пустым номером
                    {
                        Console.WriteLine("Невозможно создать машину, данные введены некорректно. Попробуйте снова.\n");
                    }
                    else
                    {
                        var car = answerType == "1" ? new Avto() : (answerType == "2" ? new Bus() : new Gruz()); //Если тип 1, то создаст автомобиль, если 2 - автобус, остальное - грузовик.
                        car.carCreation(avtoNumber, avtoFCapacity, answerType);
                        avtos.Add(car);
                        string answer = "";
                        while (answer == "")
                        {
                            Console.WriteLine("\nХотите создать еще машину? (да/нет)\n");
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
                            break;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("\nКакие-то данные были введены некорректно. Попробуйте снова.\n");
                }
            }
        }

        protected void carList(List<Avto> avtos)
        {
            int i = 1;
            foreach (Avto avto in avtos)
            {
                string type = avto._type == "1" ? "автомобиль" : (avto._type == "2" ? "автобус" : "грузовик");
                Console.Write($"\n{i}. {avto._number} - {type} - Топливо: {avto._currentFuel}/{avto._fuelCapacity} - Пробег: {avto._milleage}");
                i++;
            }
        }

        public void AVTO(List<Avto> avtos)
        {
            carStart(avtos);
            Console.WriteLine("\nВыберите машину, с которой хотите взаимодействовать:");
            string answer = "";
            int nom = -1;
            carList(avtos);
            while (true)
            {
                try
                {
                    while (answer == "")
                    {
                        if (nom == 0)
                        {
                            carList(avtos);
                        }

                        while (true)
                        {
                            Console.WriteLine("\n\nВведите номер машины из списка:\n");
                            nom = Convert.ToInt32(Console.ReadLine());
                            if (nom > 0 && nom <= avtos.Count)
                            {
                                avtos[nom - 1].commandCenter(avtos);
                                answer = " ";
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nМашины с таким номером нет в списке. Попробуйте снова.\n");
                            }
                        }

                        while (answer == " ")
                        {
                            Console.WriteLine("\nХотите вернуться к выбору машины? (да/нет)\n");
                            answer = Console.ReadLine();
                            switch (answer)
                            {
                                case "да":
                                    answer = "";
                                    nom = 0;
                                    break;
                                case "нет":
                                    answer = ".";
                                    break;
                                default:
                                    answer = " ";
                                    break;
                            }
                        }
                    }
                    if (answer == ".")
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Введено некорректное значение. Попробуйте снова.");
                }

            }
        }

        protected virtual void carCreation(string number, double fuelCapacity, string type) //Создание машины
        {
            _number = number;
            _fuelCapacity = fuelCapacity;
            _type = type;
            _fuelConsumption = 0;
            _currentFuel = 0;
            _distance = 0;
            _milleage = 0;
            _speed = 0;
            _maxSpeed = 180;
            string typeStr = _type == "1" ? "автомобиль" : (_type == "2" ? "автобус" : "грузовик");

            Console.WriteLine($"Создана машина: {typeStr}.");
        }

        protected void DisplayInfo() //Вывод информации о машине
        {
            string type = _type == "1" ? "автомобиль" : (_type == "2" ? "автобус" : "грузовик");
            Console.WriteLine($"Номер машины: {_number}\nТип: {type}\nМаксимальное количество бензина в баке: {Math.Round(_fuelCapacity, 2)} литров\nТекущее количество топлива: {Math.Round(_currentFuel, 2)} литров.\nПробег: {Math.Round(_milleage, 2)} км.");
        }

        protected virtual void speedUp() //Разгон
        {
            while (true)
            {
                try //здесь и далее try catch ловит ошибки, появляющиеся при несоответствии введенного значения заданному типу
                {
                    Console.WriteLine("\nВведите значение скорости (от 1 до 180 км/ч), до которого хотите разогнаться:\n");
                    _speed = Convert.ToDouble(Console.ReadLine());
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

        protected void FillFuel() //Заправка бака пользователем
        {
            while (true)
            {
                try
                {
                    if (_currentFuel == _fuelCapacity)
                    {
                        Console.WriteLine($"\nБак машины полон.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nСостояние бензобака: {_currentFuel}/{_fuelCapacity} л.\nВведите кол-во бензина (в литрах), на которое хотите заправить машину (макс значение:{Math.Round(_fuelCapacity, 2)}).\n");
                        double fuelAmount = Convert.ToDouble(Console.ReadLine());
                        if (_currentFuel + fuelAmount <= _fuelCapacity && fuelAmount > 0) //Условие не позволяет пользователю добавить топлива больше, чем машина может вместить, а также не позволяет ввести отрицательное значение
                        {
                            _currentFuel += fuelAmount;
                            Console.WriteLine($"\nМашина заправлена на {Math.Round(fuelAmount, 2)} литров.\nТекущее количество топлива: {Math.Round(_currentFuel, 2)} литров.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nНевозможно добавить столько топлива. Попробуйте снова.");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.");
                }
            }
        }

        protected void fuelConsumption(double speed) //рассчет расхода топлива от скорости
        {
            _fuelConsumption = speed >= 0 && speed <= 45 ? 12 : (speed > 45 && speed <= 100 ? 9 : 12.5);
        }

        protected virtual void getDistance(List<Avto> avtos)//Вычисление дистанции по кординатам и составление траектории движения
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nНеобходимо ввести начальные координаты.\nВведите \"x\":\n");
                    _startX = Convert.ToDouble(Console.ReadLine());
                    double startSample = _startX; //Переменная, дублирующая знечение начального X. Необходима для составлении траектории движения (если использовать вместо нее начальный X, то после счет при составлении списка координат будет некоррекиным)
                    Console.WriteLine("\nВведите \"y\":\n");
                    _startY = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nНеобходимо ввести конечные координаты.\nВведите \"x\":\n");
                    _endX = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("\nВведите \"y\":\n");
                    _endY = Convert.ToDouble(Console.ReadLine());

                    _distance = Math.Sqrt(Math.Pow(_endX - _startX, 2) + Math.Pow(_endY - _startY, 2)); //вычисление расстояния

                    if (_startX != _endX) //Если начальный Х не равен конечному Х, то за основу построения маршрута берется Х. У находится по формуле прямой через две точки на пл-ти
                    {
                        if (_startX < _endX)//Условия для направления движения
                        {
                            while (startSample <= _endX)
                            {
                                _coordinates.Add(Convert.ToString(startSample));
                                startSample++;
                            }
                        }
                        else
                        {
                            while (startSample >= _endX)
                            {
                                _coordinates.Add(Convert.ToString(startSample));
                                startSample--;
                            }
                        }
                        for (int i = 0; i < _coordinates.Count; i++)
                        {
                            _coordinates[i] += $"; {Convert.ToString(_startY + ((_endY - _startY) * (Convert.ToDouble(_coordinates[i]) - _startX) / (_endX - _startX)))}"; //ко всем элементам списка добавляется сооттветствующая координата Y. которая высчитывается по формуле прямой через две точки на плоскости
                        }
                    }
                    else
                    {
                        if (_startY < _endY)
                        {
                            while (_startY <= _endY)
                            {
                                _coordinates.Add($"{Convert.ToString(_startX)};{Convert.ToString(_startY)}");
                                _startY++;
                            }
                        }
                        else
                        {
                            while (_startY >= _endY)
                            {
                                _coordinates.Add($"{Convert.ToString(_startX)};{Convert.ToString(_startY)}");
                                _startY--;
                            }
                        }
                    }

                    distancePlanning(avtos); //Проверка на наличие потенциальных столкновений с другими машинами в точках координат заданного пути

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
                        Console.WriteLine("\nМаршрут запланирован успешно.");
                        break;
                    }
                    else
                    {
                        _coordinates.Clear();
                        Console.WriteLine("\nВыполнен сброс списка остановок. Начните снова с самого начала");
                    }
                }
                catch
                {
                    Console.WriteLine("\nКоординаты введены некорректно. Попробуйте снова с самого начала.");
                }
            }
        }

        protected virtual void distancePlanning(List<Avto> avtos) //Вывод потенциальных аварий
        {
            Console.WriteLine("");
            foreach (Avto avto in avtos)
            {
                foreach (string coor in _coordinates)
                {
                    foreach (string coorAnother in avto._coordinates)
                    {
                        if (coor == coorAnother && avto != this)
                        {
                            Console.WriteLine($"Внимание! Возможно столкновение с {avto._number} в точке {coor}.");
                        }
                    }
                }
            }
        }

        protected virtual void Drive(List<Avto> avtos) //Цикл езды
        {
            while (_coordinates.Count == 0) //Если маршрут не спланирован, будет вызван соответствующий метод
            {
                Console.WriteLine("\nНеобходимо запланировать маршрут");
                getDistance(avtos);
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

        protected virtual void commandCenter(List<Avto> avtos) //Главный метод
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
                        getDistance(avtos);
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