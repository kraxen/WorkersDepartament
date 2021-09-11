using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkersDepartament
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Worker
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        int id;
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int Id
        {
            get { return this.id; }
        }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string firstName { get; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string lastName { get; }
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int age { get; }
        /// <summary>
        /// Зарплата сотрудника
        /// </summary>
        public int salary { get; }
        /// <summary>
        /// Департамент, в котором работает сотрудник
        /// </summary>
        Departament departament;
        /// <summary>
        /// Департамент, в котором работает сотрудник
        /// </summary>
        public Departament Departament
        {
            get { return this.departament; }
        }
        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="firstName">Имя сотрудника</param>
        /// <param name="lastName">Фамилия сотрудника</param>
        /// <param name="age">Возраст сотрудника</param>
        /// <param name="salary">Зарплата сотрудника</param>
        public Worker(string firstName, string lastName, int age, int salary)
        {
            this.id = -1;
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.salary = salary;
            this.departament = new Departament(this);
        }
        /// <summary>
        /// Создание случайного сотрудника
        /// </summary>
        public Worker()
        {
            Random r = new Random();
            string[] names = { "Маша", "Катя", "Петя", "Вася" }; // 0-3
            string[] lastName = { "Ванко", "Ражко" }; // 0-1
            this.firstName = names[r.Next(0, 4)];
            this.lastName = lastName[r.Next(0, 2)];
            this.age = r.Next(18, 60);
            this.salary = r.Next(20, 100) * 1000;
            this.departament = new Departament(this);
        }
        /// <summary>
        /// Добавление департамента сотруднику
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departament"></param>
        public void SetDepartament(int id, Departament departament)
        {
            this.id = id;
            this.departament = departament;
        }
        /// <summary>
        /// Создание коллекции случайных сотрудников
        /// </summary>
        /// <param name="Count">Кол-во сотрудников</param>
        /// <returns></returns>
        public static List<Worker> RandomWorkerList(int Count)
        {
            List<Worker> workers = new List<Worker>();
            for (int i = 0; i < Count; i++)
            {
                workers.Add(new Worker());
            }
            return workers;
        }
        public override string ToString()
        {
            return $"{this.id}\t{this.firstName}\t{this.lastName}\t{this.age}\t{this.salary}\t{this.departament.Name}";
        }
    }
}
