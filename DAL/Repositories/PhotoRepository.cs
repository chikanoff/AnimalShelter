using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Connection;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PhotoRepository : IRepository<Photo>
    {
        private DbConnection _db;
        public PhotoRepository()
        {
            _db = new DbConnection();
        }
        public Photo Get(int id)
        {
            Photo photo = new Photo();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Photos WHERE Photos.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        photo = new Photo()
                        {
                            Id = reader.GetInt32(0),
                            PetId = reader.GetInt32(1),
                            Path = reader.GetString(2)
                        };
                    }
                }
            }
            return photo;
        }

        public IList<Photo> GetAll()
        {
            IList<Photo> photos = new List<Photo>();

            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Photos";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Photo photo = new Photo
                        {
                            Id = reader.GetInt32(0),
                            PetId = reader.GetInt32(1),
                            Path = reader.GetString(2)
                        };
                        photos.Add(photo);
                    }
                }
            }
            return photos;
        }

        public void Create(Photo item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "INSERT INTO Photos(petId, path) VALUES(@petId, @path)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@petId", item.PetId);
                cmd.Parameters.AddWithValue("@path", item.Path);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Photo item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"UPDATE Photos " +
                               $"SET Photos.petId = @petId" +
                               $"SET Photos.path = @path" +
                               $"WHERE Pets.id = {item.Id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@petId", item.PetId);
                cmd.Parameters.AddWithValue("@path", item.Path);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"DELETE FROM Photos WHERE Photos.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public int? FindPhoto(int petId, string path)
        {
            int? id = null;
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "SELECT * FROM Photos Where Photos.petId = @petId AND Photos.path = @path";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@petId", petId);
                cmd.Parameters.AddWithValue("@path", path);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }

            return id;
        }
    }
}
