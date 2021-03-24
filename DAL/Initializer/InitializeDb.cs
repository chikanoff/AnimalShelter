using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL.Initializer
{
    public static class InitializeDb
    {
        public static void Initialize()
        {
            using(SqlConnection conn = new SqlConnection("Server=DESKTOP-3DPM8BP\\SQLEXPRESS;Database=AnimalShelter;Trusted_Connection=True;"))
            {
                conn.Open();
                for(int i = 0; i < 10; i++)
                {
                    string query = "insert into Breeds " +
                        "values(@breed)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@breed", "breed" + i);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
