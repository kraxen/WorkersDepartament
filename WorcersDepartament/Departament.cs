using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WorkersDepartament
{
    /// <summary>
    /// Департамент сотрудников
    /// </summary>
    public class Departament
    {
        /// <summary>
        /// Название департамента
        /// </summary>
        string name;
        /// <summary>
        /// Название департамента
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }
        /// <summary>
        /// Дата создания департамента
        /// </summary>
        public DateTime dateAdded { get; }
        /// <summary>
        /// Кол-во сотрудников
        /// </summary>
        int workersCount;
        /// <summary>
        /// Индекс последнего сотрудника
        /// </summary>
        int index;
        /// <summary>
        /// Кол-во сотрудников
        /// </summary>
        public int WorkersCount 
        {
            get { return this.workersCount; }
        }
        /// <summary>
        /// Коллекция сотрудников
        /// </summary>
        List<Worker> workers;
        /// <summary>
        /// Коллекция сотрудников
        /// </summary>
        public List<Worker> Workers { get { return this.workers; } }
        /// <summary>
        /// Создание департамента
        /// </summary>
        /// <param name="name">Имя департамента</param>
        /// <param name="workers">Коллекция работников департамента</param>
        public Departament(string name, List<Worker> workers)
        {
            this.dateAdded = DateTime.Now;
            this.name = name;
            this.workers = workers;
            this.workersCount = workers.Count;
            this.index = workers.Count;

            for (int i = 0; i < workers.Count; i++)
            {
                workers[i].SetDepartament(i, this);
            }
        }
        /// <summary>
        /// Создание пустого департамента
        /// </summary>
        /// <param name="worker">Сотрудник без департамента</param>
        public Departament(Worker worker)
        {
            this.name = "Без департамента";
            this.dateAdded = DateTime.Now;
            this.workers = new List<Worker>() { worker };
            this.workersCount = 1;
            worker.SetDepartament(-1, this);
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        public void AddWorker(Worker worker)
        {
            this.workers.Add(worker);
            this.workersCount++;
            this.index++;
            worker.SetDepartament(index-1, this);
        }
        /// <summary>
        /// Удаление сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        public void RemoveWorker(int id)
        {
            this.workers.RemoveAt(id);
            this.workersCount--;
        }
        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <param name="worker">Новое описание сотрудника</param>
        public void UpdateWorker(int id, Worker worker)
        {
            worker.SetDepartament(id, this);
            for(int i = 0; i < workers.Count; i++)
            {
                if(workers[i].Id == id)
                {
                    workers[i] = worker;
                    break;
                }
            }
        }
        /// <summary>
        /// Изменение названия департамента
        /// </summary>
        /// <param name="newName"></param>
        public void UpdateDepartamentName(string newName)
        {
            this.name = newName;
        }
        /// <summary>
        /// Сортировка сотрудников по имени сотрудника
        /// </summary>
        public void SortByFirstName()
        {
            this.workers = this.workers.OrderBy(e => e.firstName).ToList();
        }
        /// <summary>
        /// Сортировка сотрудников по фамилии сотрудника
        /// </summary>
        public void SortByLastName()
        {
            this.workers = this.workers.OrderBy(e => e.lastName).ToList();
        }
        /// <summary>
        /// Сортировка сотрудников по возрасту
        /// </summary>
        public void SortByAge()
        {
            this.workers = this.workers.OrderBy(e => e.age).ToList();
        }
        /// <summary>
        /// Сортировка сотрудников по зарплате
        /// </summary>
        public void SortBySalary()
        {
            this.workers = this.workers.OrderBy(e => e.salary).ToList();
        }
        /// <summary>
        /// Сохранить департамент в XML
        /// </summary>
        public void SaveAsXML(string path)
        {
            XElement departament = new XElement("DEPARTAMENT");
            departament.Add(new XAttribute("NAME", this.name));
            departament.Add(new XAttribute("DATA_ADDED", this.dateAdded));
            departament.Add(new XAttribute("WORKERS_COUNT", this.workersCount));
            XElement workers = new XElement("WORKERS");
            foreach (var e in this.workers)
            {
                XElement worker = new XElement("WORKER");
                worker.Add(new XAttribute("Id", e.Id));
                worker.Add(new XAttribute("firstName", e.firstName));
                worker.Add(new XAttribute("lastName", e.lastName));
                worker.Add(new XAttribute("age", e.age));
                worker.Add(new XAttribute("salary", e.salary));
                workers.Add(worker);
            }
            departament.Add(workers);
            departament.Save(path);
            Console.WriteLine($"Файл {path} успешно сохранен");
        }
        /// <summary>
        /// Сохранение в формате Json
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void SaveAsJson(string path)
        {
            JObject departament = new JObject();
            departament["name"] = this.name;
            departament["data Added"] = this.dateAdded;
            departament["workers Count"] = this.workersCount;
            JArray workers = new JArray();
            foreach(var e in this.workers)
            {
                workers.Add(new JObject()
                {
                    ["Id"] = e.Id,
                    ["firstName"] = e.firstName,
                    ["lastName"] = e.lastName,
                    ["age"] = e.age,
                    ["salary"] = e.salary                    
                });
            }
            departament["workers"] = workers;

            File.WriteAllText(path, departament.ToString());

            Console.WriteLine($"Файл {path} успешно сохранен");
        }

        public override string ToString()
        {
            string result = "Название департамента\tДата создания\tКоличество сотрудников\n";
            result += this.name + "\t" + this.dateAdded + "\t" + this.WorkersCount + "\nСписок сотрудников:\n";
            result += $"\tId\tИмя\tФамилимя\tВозраст\tЗарплата\tНазвание департамента\n";
            foreach(var e in this.workers)
            {
                result += "\t" + e.ToString() + "\n";
            }
            return result;
        }
    }
}
