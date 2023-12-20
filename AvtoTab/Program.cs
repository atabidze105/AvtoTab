using AvtoTab;

class Program
{
    static void Main()
    {
        //List<Avto> avtos = new();
        //Avto idle = new();
        //idle.AVTO(avtos);

        List<List<string>> test1 = new();
        List<List<string>> test2 = new();
        List<string> test3 = new();
        List<string> test4 = new();
        List<string> test5 = new();

        test1.Add(test3);
        test1.Add(test4);
        test1.Add(test5);

        for (int j = 0; 0 <= Math.Floor(Convert.ToDouble(test1.Count)/2); j++)
        {
            test2.Add(test1[j]);
        }
    }
}