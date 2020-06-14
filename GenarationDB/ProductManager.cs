using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GenarationDB
{
    public class ProductManager
    {
        private readonly SqlConnection _con;
        public ProductManager(SqlConnection con)
        {
            _con = con;
        }
        public void Show()
        {
            string sql = "SELECT Id, Name FROM viewProducts";
            SqlCommand comm = _con.CreateCommand();
            comm.CommandText = sql;
            using (var reader = comm.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("Id: {0}", reader["Id"]);
                    Console.WriteLine("Name: {0}", reader["Name"]);
                }
            }
        }

        public void InsertCategoriesRandomSP()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Description");

            dt.Rows.Add("Ноутбуки", "Для іграків");
            dt.Rows.Add("ПК", "Два гіга два ядра");
            dt.Rows.Add("Телефони", "Туплять у вайберів і менше у телеграмі");

            SqlCommand comm = new SqlCommand("spInsertCategoryList", _con);
            comm.CommandType = CommandType.StoredProcedure;
            SqlParameter pCatList = new SqlParameter()
            {
                ParameterName = "@ListInsert",
                Value = dt
            };
            comm.Parameters.Add(pCatList);

            comm.ExecuteNonQuery();
            
        }

        public void TransactionTest()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string constrConf= ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
                    var strCon = $"{constrConf};Initial Catalog=shop";
                    using (SqlConnection con = new SqlConnection(strCon))
                    {
                        // Opening the connection automatically enlists it in the 
                        // TransactionScope as a lightweight transaction.
                        con.Open();
                        string sql = "INSERT INTO tblProducts ([Name],[SupplierId],[CategoryId],[Unit],[Price]) " +
                        "values(N'Чоботи', '1', '1', '10 boxes x 20 bags', 200);";
                        SqlCommand command = con.CreateCommand();
                        command.CommandText = sql;
                        var res = command.ExecuteNonQuery(); //посилаємо команду в БД
                        Console.WriteLine("---add res {0}------", res);

                        sql = "SELECT SCOPE_IDENTITY() as Id";
                        command.CommandText = sql;
                        int id = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = int.Parse(reader["Id"].ToString());
                                Console.WriteLine("Insted Id: {0}", reader["Id"]);
                            }
                        }

                        sql = $"UPDATE [dbo].[tblProducts] SET [Name] = N'Вареник' WHERE Id={id}";

                        command.CommandText = sql;
                        res = command.ExecuteNonQuery(); //посилаємо команду в БД
                        Console.WriteLine("---update res {0}------", res);

                        //throw new Exception("Отвал");

                        //sql = $"DELETE FROM [dbo].[tblProducts] WHERE Id={id} ";

                        //command.CommandText = sql;
                        //res = command.ExecuteNonQuery(); //посилаємо команду в БД
                        //Console.WriteLine("---del res {0}------", res);
                        
                    }
                    scope.Complete();
                }
            }
            catch (TransactionAbortedException ex)
            {

                Console.WriteLine("---Проблема у роботі транзакції---\n {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("---Проблема виконання операції---\n {0}", ex.Message);
            }
        }


    }
}
