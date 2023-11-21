namespace AvtoTab
{
    public class Avto
    {
        private string _number; //Номер машины
        private double _fuelCapacity; //Макс кол-во бензина в баке
        private double _fuelConsumption; //Расход топлива
        private double _currentFuel; //Текущее кол-во бензина
        private double _distance;//расстояние
        private double _milleage;//Пробег

        private void carCreation(string number, double fuelCapacity, double fuelConsumption) //Создание машины
        {
            _number = number;
            _fuelCapacity = fuelCapacity;
            _fuelConsumption = fuelConsumption;
            _currentFuel = 0;
            _distance = 0;
            _milleage = 0;
            Console.WriteLine("Машина создана успешно.");
        }

        private void DisplayInfo() //Вывод информации о машине
        {
            Console.WriteLine($"Номер машины: {_number}\nКоличество бензина в баке: {Math.Round(_fuelCapacity, 2)} литров\nРасход топлива на 100 км: {Math.Round(_fuelConsumption, 2)} литров\nТекущее количество топлива: {Math.Round(_currentFuel, 2)} литров.\nПробег: {Math.Round(_milleage, 2)} км.");
        }

        private void FillFuel() //Заправка бака пользователем
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

        private void getDistance()//Высчитывание дистанции по кординатам
        {
            Console.WriteLine("\nВведите начальные и конечные координаты в формате \"x1 y1 x2 y2\".");
            string disString = Console.ReadLine();
            string[] dist = disString.Split(' '); //Разбиение строки на элементы массива

            _distance = Math.Sqrt(Math.Pow(Convert.ToDouble(dist[2]) - Convert.ToDouble(dist[0]), 2) + Math.Pow(Convert.ToDouble(dist[3]) - Convert.ToDouble(dist[1]), 2)); //вычисление расстояния
        }

        private void Drive() //Цикл езды
        {
            while (Math.Floor(_currentFuel) == 0) //Если у машины нет топлива, произойдет обращение к методу заправки
            {
                Console.WriteLine("\nНевозможно начать поездку с пустым бензобаком.\nТребуется дозаправка");
                FillFuel();
            }
            
            double fuelDistance = _currentFuel / (_fuelConsumption / 100); //Расстояние, которое может проехать машина с заправленным баком
            getDistance();
            Console.WriteLine($"\nНеобходимо проехать {_distance} км.\n\nНачало поездки.");
            _distance -= fuelDistance;

            while (_distance > 0) //Цикл езды
            {
                _currentFuel = 0; //обнуление кол-ва топлива
                _milleage += fuelDistance; //Увеличение пробега

                Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nОсталось ехать {Math.Round(_distance, 2)} км.\nТребуется дозаправка.");
                FillFuel(); //Обращение к методу заправки
                fuelDistance = _currentFuel / (_fuelConsumption / 100); //Обновление расстояния, котрое может проехать машина с заправленным на текущее кол-во топлива баком
                _distance -= fuelDistance; //Обновление расстояния, которое необходимо проехать
            }

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
                Console.WriteLine("Введите расход топлива на 100 км (в литрах):");
                double avtoFConsumption = Convert.ToDouble(Console.ReadLine());
                foreach (Avto avto in avtos) //Проверка на уникальность номера
                {
                    if (avtoNumber == avto._number)
                    {
                        avtoNumber = "";
                        Console.WriteLine("\nВведенный номер занят.");
                        break;
                    }
                }
                if (avtoNumber == "" || avtoFCapacity <= 0 || avtoFConsumption <= 0) //Условие не позволяет создать аккаунт с пустым номером
                {
                    Console.WriteLine("Невозможно создать машину, данные введены некорректно.\n");
                }
                else
                {
                    carCreation(avtoNumber, avtoFCapacity, avtoFConsumption);
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