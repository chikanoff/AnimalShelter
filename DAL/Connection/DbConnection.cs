using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Connection
{
    public class DbConnection
    {
        private readonly string _connectionString;
        public DbConnection()
        {
            _connectionString = "Server=LAPTOP-L2ADI7AU\\TEW_SQLEXPRESS;Database=AnimalShelter;Trusted_Connection=True;";
        }

        public SqlConnection ConnectToDatabase()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
