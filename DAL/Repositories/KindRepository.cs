using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Connection;

namespace DAL.Repositories
{
    public class KindRepository : IRepository<Kind>
    {
        private DbConnection _db;
        public KindRepository()
        {
            _db = new DbConnection();
        }
        public void Create(Kind item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "INSERT INTO Kinds VALUES(@name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"DELETE FROM Kinds WHERE Kinds.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Kind Get(int id)
        {
            Kind kind = new Kind();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Kinds WHERE Kinds.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kind = new Kind
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
            return kind;
        }
        public IList<Kind> GetAll()
        {
            IList<Kind> kinds = new List<Kind>();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "SELECT * FROM Kinds";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Kind kind = new Kind
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        kinds.Add(kind);
                    }
                }
            }
            return kinds;
        }

        public void Update(Kind item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"UPDATE Kinds SET kind = @name WHERE Kinds.id = {item.Id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
