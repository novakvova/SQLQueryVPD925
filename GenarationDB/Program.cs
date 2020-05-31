using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenarationDB
{
    class Program
    {
        static string conSTR = 
            "Data Source=DESKTOP-HEB4007;Integrated Security=True";
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            int action = 0;
            DatabaseManager dbManager = new DatabaseManager(conSTR);
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Створити БД");
                Console.WriteLine("2. Видалити БД");
                Console.WriteLine("3. Показати список БД");
                Console.WriteLine("4. Підключитися до БД(створити таблиці, видалити, ..)");
                Console.WriteLine("5. Запис множини даних в tblCustomers");
                action = int.Parse(Console.ReadLine());
                switch(action)
                {
                    case 1:
                        {
                            try
                            {
                                string name;
                                Console.WriteLine("Введіть назву БД:");
                                name = Console.ReadLine();
                                dbManager.CreateDB(name);
                                Console.WriteLine("------Базу даних успішно створено----");
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("Помилка створення БД --", ex.Message);
                            }
                            break;
                        }
                    case 2:
                        {
                            string name;
                            Console.WriteLine("Введіть назву БД:");
                            name = Console.ReadLine();
                            dbManager.DeleteDB(name);
                            Console.WriteLine("------Базу даних успішно видалено----");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Список БД:");
                            dbManager.ShowAllDatabase();
                            Console.WriteLine("------Базу даних успішно видалено----");
                            break;
                        }

                    case 4:
                        {
                            string dbName;
                            Console.WriteLine("Введіть назву БД:");
                            dbName = Console.ReadLine();
                            string conectionSTR = 
                                $"{conSTR};Initial Catalog={dbName}";
                            TableManager tableManager = new TableManager(conectionSTR);
                            tableManager.CreateTabels();
                            tableManager.SeederDatabase();

                            break;
                        }
                    case 5:
                        {
                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();
                            string dbName;
                            Console.WriteLine("Введіть назву БД:");
                            dbName = Console.ReadLine();
                            string conectionSTR =
                                $"{conSTR};Initial Catalog={dbName}";

                            TableManager tableManager = new TableManager(conectionSTR);
                            Console.WriteLine("Сказіть кількість клієнтів");
                            int n = int.Parse(Console.ReadLine());
                            tableManager.RandomInsertCustomers(n);
                            stopWatch.Stop();
                            // Get the elapsed time as a TimeSpan value.
                            TimeSpan ts = stopWatch.Elapsed;

                            // Format and display the TimeSpan value.
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts.Hours, ts.Minutes, ts.Seconds,
                                ts.Milliseconds / 10);
                            Console.WriteLine("RunTime " + elapsedTime);
                            break;
                        }
                }
            } while (action!=0);
        }

        
    }
}
