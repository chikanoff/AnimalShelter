using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Connection;

namespace DAL.Repositories
{
    public class BreedRepository : IRepository<Breed>
    {
        private DbConnection _db;
        public BreedRepository()
        {
            _db = new DbConnection();
        }
        public void Create(Breed item)
        {
            using(SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "INSERT INTO Breeds VALUES(@name)";
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
                string query = $"DELETE FROM Breeds WHERE Breeds.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public Breed Get(int id)
        {
            Breed breed = new Breed();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Breeds WHERE Breeds.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        breed = new Breed
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
            return breed;
        }
        public IList<Breed> GetAll()
        {
            IList<Breed> breeds = new List<Breed>();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "SELECT * FROM Breeds";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Breed breed = new Breed
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        breeds.Add(breed);
                    }
                }
            }
            return breeds;
        }

        public void Update(Breed item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"UPDATE Breeds SET name = @name WHERE Breeds.id = {item.Id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
