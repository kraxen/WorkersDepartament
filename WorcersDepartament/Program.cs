using System;

namespace WorkersDepartament
{
    class Program
    {
        static void Main(string[] args)
        {
            Departament d1 = new Departament("Departament 1", Worker.RandomWorkerList(100));
            //d1.SortBySalary();
            //d1.SortByFirstName();
            //d1.SortByLastName();
            d1.SortByAge();
            d1.AddWorker(new Worker());
            d1.RemoveWorker(15);
            Worker w1 = new Worker("Саша", "Кузнецов", 15, 15000);
            d1.AddWorker(w1);
            d1.UpdateWorker(98, new Worker("Petr", "Ivanov", 30, 15000));

            Console.WriteLine(d1);
            d1.SaveAsXML($"{d1.Name}.xml");
            d1.SaveAsJson($"{d1.Name}.json");

            Console.ReadKey();
        }
    }
}
