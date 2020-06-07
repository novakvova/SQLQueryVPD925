using Bogus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenarationDB
{
    public class TableManager
    {
        private readonly SqlConnection con;
        private string dirSql = "sql";

        public TableManager(string connStr)
        {
            con = new SqlConnection(connStr);
            con.Open();
        }

        public void CreateTabels()
        {
            //Назви категорій
            ExecuteCommandFromFile("tblCategories.sql");
            //Постачальники товару
            ExecuteCommandFromFile("tblSuppliers.sql");
            //Клієнти
            ExecuteCommandFromFile("tblCustomers.sql");
            //Продукти
            ExecuteCommandFromFile("tblProducts.sql");
            //Працівники
            ExecuteCommandFromFile("tblEmployees.sql");
            //Cлужба доставки
            ExecuteCommandFromFile("tblShippers.sql");
            //Замовлення
            ExecuteCommandFromFile("tblOrders.sql");
            //Список товарів у заказі
            ExecuteCommandFromFile("tblOrdersDetails.sql");
            //Створити viewProducts
            ExecuteCommandFromFile("viewProducts.sql");

            //Створити CategoryType
            ExecuteCommandFromFile("CategoryType.sql");

            //Створити spInsertCategoryList
            ExecuteCommandFromFile("spInsertCategoryList.sql");
        }

        /// <summary>
        /// Заповнити БД значеннями до замовчюванню
        /// </summary>
        public void SeederDatabase()
        {
            //Закинути в БД дефолтні значення
            ExecuteCommandFromFile("seeder.sql");
        }

        public void RandomInsertCustomers(int count)
        {
            var customFaker = new Faker<Customer>("uk")
                .RuleFor(c => c.Name, f => f.Name.FullName())
                .RuleFor(c => c.Address, f => f.Address.StreetAddress())
                .RuleFor(c => c.Country, f => f.Address.Country())
                .RuleFor(c => c.City, f => f.Address.City());


            //string sql = "INSERT[dbo].[tblCustomers]([Name], [Address], [City], [Country]) " +
            //    "VALUES(N'Alfreds Futterkiste', N'Obere Str. 57', N'Berlin', N'Germany')";
            for (int i = 0; i < count; i++)
            {
                var cust = customFaker.Generate();
                string sql = "INSERT[dbo].[tblCustomers]([Name], [Address], [City], [Country]) " +
                        "VALUES(@Name, @Address, @City, @Country)";
                //Console.WriteLine(cust);
                SqlCommand command = con.CreateCommand();
                command.Parameters.AddWithValue("@Name", cust.Name);
                command.Parameters.AddWithValue("@Address", cust.Address);
                command.Parameters.AddWithValue("@City", cust.City);
                command.Parameters.AddWithValue("@Country", cust.Country);
                command.CommandText = sql;
                command.ExecuteNonQuery(); //посилаємо команду в БД
            }

        }

        public SqlConnection GetSqlConnection { get { return con; } } 

        private void ExecuteCommandFromFile(string file)
        {
            string sql = ReadSqlFile(file);
            SqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery(); //посилаємо команду в БД
        }

        private string ReadSqlFile(string file)
        {
            string sql=File.ReadAllText($"{dirSql}\\{file}");
            return sql;
        }


    }
}
