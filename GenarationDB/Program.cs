using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                action = int.Parse(Console.ReadLine());
                switch(action)
                {
                    case 1:
                        {
                            string name;
                            Console.WriteLine("Введіть назву БД:");
                            name = Console.ReadLine();
                            dbManager.CreateDB(name);
                            Console.WriteLine("------Базу даних успішно створено----");
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
            } while (action!=0);
        }

        
    }
}
