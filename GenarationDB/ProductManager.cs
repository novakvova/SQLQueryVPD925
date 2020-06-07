using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var reader = comm.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine("Id: {0}", reader["Id"]);
                Console.WriteLine("Name: {0}", reader["Name"]);
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

    }
}
