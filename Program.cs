using AvtoTab;

class Program
{
    static void Main()
    {
        void accountChoshing(Avto[] avtos, int quantity) //Метод для выбора из массива объекта для применения метода
        {
            int nom = -1;
            //int i = 1;
            Console.WriteLine("\nНеобходимо выбрать автомобиль чтобы продолжить.");
            //foreach (Avto avto in avtos) 
            //{
            //    Console.WriteLine($"{i}. {avto.Number}");
            //    i++;
            //}
            while (nom > avtos.Length || nom < 0)
            {
                Console.WriteLine($"Введите один из доступных номеров:\n\nот 1 до {quantity}:\n");
                nom = Convert.ToInt32(Console.ReadLine()); //Выбор индекса элемента массива
                if (nom > 0)
                {
                    if (nom <= avtos.Length)
                    {
                        avtos[nom - 1].commandCenter(avtos);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка. Введите значение в заданом диапазоне.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка. Введите значение в заданом диапазоне.\n");
                }
            }
        }


        Console.WriteLine("Чтобы начать работу, необходимо задать количество машин.");
        int quantityOfMachines = 0;
        while (quantityOfMachines <= 0)
        {
            Console.WriteLine("Введите количество машин, которое хотите создать:\n");
            quantityOfMachines = Convert.ToInt32(Console.ReadLine());
            if (quantityOfMachines <=0 )
            {
                Console.WriteLine("\nВведите значение больше нуля\n");
                quantityOfMachines = 0;
            }
        }

        Avto[] machines = new Avto[quantityOfMachines];
        for (int i = 0; i < machines.Length; i++) //Цикл для создания элементов массива
        {
            machines[i] = new Avto();
        }

        accountChoshing(machines, quantityOfMachines); //Первое обращение к методу        

        Console.WriteLine("\nЧтобы вернуться к выбору машины, нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".\n");
        string thisMachine = ""; //Переменная для поддержания работы следующего цикла
        thisMachine = Console.ReadLine();

        if (thisMachine == "")
        {
            while (thisMachine == "")
            {
                accountChoshing(machines, quantityOfMachines);
                Console.WriteLine("\nЧтобы вернуться к выбору машины, нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".\n");
                thisMachine = Console.ReadLine();
            } //Метод работает пока строка пуста
        }
    }
}