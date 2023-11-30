namespace AvtoTab
{
    internal class Avto
    {
        protected string _number; //Номер машины
        protected double _fuelCapacity; //Макс кол-во бензина в баке
        protected double _fuelConsumption; //Расход топлива
        protected double _currentFuel; //Текущее кол-во бензина
        protected double _distance;//расстояние
        protected double _milleage;//Пробег
        protected double _speed; //Скорость
        protected double _maxSpeed; //Максимальная скорость

        public string Number
        {
            get { return _number; }
        }


        protected virtual void carCreation(string number, double fuelCapacity) //Создание машины
        {
            _number = number;
            _fuelCapacity = fuelCapacity;
            _fuelConsumption = 0;
            _currentFuel = 0;
            _distance = 0;
            _milleage = 0;
            _speed = 0;
            _maxSpeed = 180;
            Console.WriteLine("Машина создана успешно.");
        }

        protected void DisplayInfo() //Вывод информации о машине
        {
            Console.WriteLine($"Номер машины: {_number}\nКоличество бензина в баке: {Math.Round(_fuelCapacity, 2)} литров\nРасход топлива на 100 км: {Math.Round(_fuelConsumption, 2)} литров\nТекущее количество топлива: {Math.Round(_currentFuel, 2)} литров.\nПробег: {Math.Round(_milleage, 2)} км.");
        }

        protected void FillFuel() //Заправка бака пользователем
        {
            Console.WriteLine($"\nВведите кол-во бензина (в литрах), на которое хотите заправить машину (макс значение:{Math.Round(_fuelCapacity, 2)}).\n");
            double fuelAmount = Convert.ToDouble(Console.ReadLine());
            if (_currentFuel + fuelAmount <= _fuelCapacity && fuelAmount > 0) //Условие не позволяет пользователю добавить топлива больше, чем машина может вместить, а также не позволяет ввести отрицательное значение
            {
                _currentFuel += fuelAmount;
                Console.WriteLine($"\nМашина заправлена на {Math.Round(fuelAmount, 2)} литров.\nТекущее количество топлива: {Math.Round(_currentFuel, 2)} литров.");
            }
            else
            {
                Console.WriteLine("\nНевозможно добавить столько топлива.");
            }
        }
        

        protected void speedUp() //Разгон
        {
            while (true)
            {
                try //здесь try catch ловит ошибки, появляющиеся при несоответствии введенного значения типу double
                {
                    Console.WriteLine("Введите значение скорости (от 1 до 180 км/ч), до которого хотите разогнаться:");
                    _speed = Convert.ToDouble(Console.ReadLine());
                    if (_speed > 0 && _speed <= 180)
                    {
                        fuelConsumption(_speed);
                        break; //Выход из цикла
                    }
                    else
                    {
                        Console.WriteLine("Введено значение вне заданного диапазона. Попробуйте снова.\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Введено некорректное значение. Попробуйте снова.\n");
                }
            }
        }

        protected void fuelConsumption(double speed) //рассчет расхода топлива от скорости
        {
            if (speed >= 0 && speed <= 45)
            {
                _fuelConsumption = 12;
            }
            else
            {
                if (speed > 45 && speed <= 100)
                {
                    _fuelConsumption = 9;
                }
                else
                {
                    if (speed > 100 && speed <= 180)
                    {
                        _fuelConsumption = 12.5;
                    }
                }
            }
        }

        protected void getDistance()//Высчитывание дистанции по кординатам
        {
            Console.WriteLine("\nВведите начальные координаты в формате \"x1 y1 x2 y2\".");
            string disString = Console.ReadLine();
            string[] dist = disString.Split(' '); //Разбиение строки на элементы массива

            _distance = Math.Sqrt(Math.Pow(Convert.ToDouble(dist[2]) - Convert.ToDouble(dist[0]), 2) + Math.Pow(Convert.ToDouble(dist[3]) - Convert.ToDouble(dist[1]), 2)); //вычисление расстояния
        }

        protected void Drive() //Цикл езды
        {
            while (Math.Floor(_currentFuel) == 0) //Если у машины нет топлива, произойдет обращение к методу заправки
            {
                Console.WriteLine("\nНевозможно начать поездку с пустым бензобаком.\nТребуется дозаправка");
                FillFuel();
            }

            getDistance();
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


        }

        public void commandCenter(Avto[] avtos) //Главный метод
        {
            while (_number == null || _number == "") //Если у полей объекта нет значений, то в цикле они ему присваиваются
            {
                Console.WriteLine("\nНеобходимо создать машину чтобы продолжить.\nВведите номер машины:");
                string avtoNumber = Console.ReadLine();
                Console.WriteLine("Введите вместимость бензобака (в литрах):");
                double avtoFCapacity = Convert.ToDouble(Console.ReadLine());
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
                    Console.WriteLine("Невозможно создать машину, данные введены некорректно.\n");
                }
                else
                {
                    carCreation(avtoNumber, avtoFCapacity);
                }
            }


            Console.WriteLine($"\n\nДобро пожаловать на панель управления машины {_number}.\n");
            string continuation = "";
            while (continuation == "")
            {
                Console.WriteLine(" Чтобы узнать информацию о машине, выберите \"1\".\n Чтобы заправить машину, выберите \"2\".\n Чтобы начать поездку, выберите \"3\".\n\n");
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
                        Drive();
                        break;
                    default:
                        Console.WriteLine("\nКоманды с таким номером не существует.");
                        break;
                }
                Console.WriteLine("\nЧтобы продолжить нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".");
                continuation = Console.ReadLine();
            }
        }


    }
}