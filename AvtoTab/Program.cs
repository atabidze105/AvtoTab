using AvtoTab;

class Program
{
    static void Main()
    {
        void accountChoshing(Avto[] avtos, int quantity) //Метод для выбора из массива объекта для применения метода
        {
            Console.WriteLine($"Необходимо выбрать автомобиль чтобы продолжить. Введите один из доступных номеров:\n\nот 1 до {quantity}:\n");
            int nom = Convert.ToInt32(Console.ReadLine()) - 1; //Выбор индекса элемента массива
            avtos[nom].commandCenter();
        }

        Console.WriteLine("Чтобы начать работу, необходимо задать количество машин.\nВведите количество машин, которое хотите создать:");
        int quantityOfMachines = Convert.ToInt32(Console.ReadLine());
        Avto[] machines = new Avto[quantityOfMachines];
        for (int i = 0; i < machines.Length; i++) //Цикл для создания элементов массива
        {
            machines[i] = new Avto();
        }

        accountChoshing(machines, quantityOfMachines); //Первое обращение к методу        

        Console.WriteLine("\nЧтобы вернуться к выбору машины, нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".");
        string thisMachine = ""; //Переменная для поддержания работы следующего цикла
        thisMachine = Console.ReadLine();

        if (thisMachine == "")
        {
            do
            {
                accountChoshing(machines, quantityOfMachines);
                Console.WriteLine("Чтобы вернуться к выбору машины, нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".");
                thisMachine = Console.ReadLine();
            } while (thisMachine == ""); //Метод работает пока строка пуста
        }
    }
}