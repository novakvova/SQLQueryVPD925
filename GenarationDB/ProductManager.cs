using System;
using System.Collections.Generic;
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

    }
}
