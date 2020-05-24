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
            int action = 0;
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Створити БД");
                action = int.Parse(Console.ReadLine());
                switch(action)
                {
                    case 1:
                        {
                            string name;
                            Console.WriteLine("Введіть назву БД:");
                            name = Console.ReadLine();
                            CreateDB(name);
                            Console.WriteLine("------Базу даних успішно створено----");
                            break;
                        }
                }
            } while (action!=0);
        }

        static void CreateDB(string name)
        {
            string sql = $"CREATE DATABASE {name};";
            using (SqlConnection con =new SqlConnection(conSTR))
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery(); //посилаємо команду в БД
                
            }

        }
    }
}
