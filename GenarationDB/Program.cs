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
            
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Керування базами даних");
                Console.WriteLine("2. Керування окремою БД");
                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            WorkDatabases();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Введіть назву БД:");
                            string dbName = Console.ReadLine();
                            WorkTabelsInDB(dbName);
                            break;
                        }
                    
                }
            } while (action!=0);
        }

        //Керування Базами даних
        static void WorkDatabases()
        {
            DatabaseManager dbManager = new DatabaseManager(conSTR);
            int action = 0;
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Створити БД");
                Console.WriteLine("2. Видалити БД");
                Console.WriteLine("3. Показати список БД");
                action = int.Parse(Console.ReadLine());
                switch (action)
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
                            catch (Exception ex)
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
                }
            } while (action != 0);
        }

        //Керування окремою Базою даних
        static void WorkTabelsInDB(string dbName)
        {
            string conectionSTR = $"{conSTR};Initial Catalog={dbName}";
            TableManager tableManager = new TableManager(conectionSTR);
            int action = 0;
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Cтворити таблиці");
                Console.WriteLine("2. Заповнити БД по замовчюванню");
                Console.WriteLine("3. Заповнити даними BogusRandom");
                Console.WriteLine("4. Показати продукти");
                Console.WriteLine("5. Заповнити даними за допомогою SP");
                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            tableManager.CreateTabels();
                            break;
                        }
                    case 2:
                        {
                            tableManager.SeederDatabase();
                            break;
                        }
                    case 3:
                        {
                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();
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
                    case 4:
                        {
                            var con = tableManager.GetSqlConnection;
                            ProductManager pm = new ProductManager(con);
                            Console.WriteLine("------Продукти БД--------");
                            pm.Show();
                            break;
                        }
                    case 5:
                        {
                            var con = tableManager.GetSqlConnection;
                            ProductManager pm = new ProductManager(con);
                            Console.WriteLine("------Додавання трьох категорій--------");
                            pm.InsertCategoriesRandomSP();
                            break;
                        }
                }
            } while (action != 0);
        }

    }
}
