using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Connection;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PetRepository : IRepository<Pet>
    {

        private DbConnection _db;
        public PetRepository()
        {
            _db = new DbConnection();
        }
        public Pet Get(int id)
        {
            Pet pet = new Pet();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Pets WHERE Pets.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pet = new Pet
                        {
                            Id = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                            KindId = reader.GetInt32(2),
                            BreedId = reader.GetInt32(3),
                            Conditions = reader.GetString(4),
                            ArrivalDate = reader.GetDateTime(5),
                            Color = reader.GetString(6),
                            HealthStatus = reader.GetString(7)
                        };
                    }
                }
            }
            return pet;
        }

        public IList<Pet> GetAll()
        {
            IList<Pet> pets = new List<Pet>();

            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Pets";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            Id = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                            KindId = reader.GetInt32(2),
                            BreedId = reader.GetInt32(3),
                            Conditions = reader.GetString(4),
                            ArrivalDate = reader.GetDateTime(5),
                            Color = reader.GetString(6),
                            HealthStatus = reader.GetString(7)
                        };
                        pets.Add(pet);
                    }
                }
            }
            return pets;
        }

        public IList<Pet> GetUserPets(int userId)
        {
            IList<Pet> pets = new List<Pet>();

            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Pets as p JOIN Operations as o ON p.Id = o.petId WHERE o.userId = @userId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            Id = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                            KindId = reader.GetInt32(2),
                            BreedId = reader.GetInt32(3),
                            Conditions = reader.GetString(4),
                            ArrivalDate = reader.GetDateTime(5),
                            Color = reader.GetString(6),
                            HealthStatus = reader.GetString(7)
                        };
                        pets.Add(pet);
                    }
                }
            }
            return pets;
        }

        public IList<Pet> GetAvailablePets()
        {
            IList<Pet> pets = new List<Pet>();

            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"select * from Pets where id not in (select p.id from Pets as p join Operations o on o.petId = p.id)";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Pet pet = new Pet
                        {
                            Id = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                            KindId = reader.GetInt32(2),
                            BreedId = reader.GetInt32(3),
                            Conditions = reader.GetString(4),
                            ArrivalDate = reader.GetDateTime(5),
                            Color = reader.GetString(6),
                            HealthStatus = reader.GetString(7)
                        };
                        pets.Add(pet);
                    }
                }
            }
            return pets;
        }

        public void Create(Pet item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "INSERT INTO Pets(nickname, kindId, breedId, conditions, arrivalDate, color, healthStatus) " +
                               "VALUES(@nickname, @kindId, @breedId, @conditions, @arrivalDate, @color, @healthStatus)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nickname", item.Nickname);
                cmd.Parameters.AddWithValue("@kindId", item.KindId);
                cmd.Parameters.AddWithValue("@breedId", item.BreedId);
                cmd.Parameters.AddWithValue("@conditions", item.Conditions);
                cmd.Parameters.AddWithValue("@arrivalDate", item.ArrivalDate);
                cmd.Parameters.AddWithValue("@color", item.Color);
                cmd.Parameters.AddWithValue("@healthStatus", item.HealthStatus);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Pet item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"UPDATE Pets " +
                               $"SET Pets.nickname = @nickname, " +
                               $" Pets.kindId = @kindId, " +
                               $" Pets.breedId = @breedId, " +
                               $" Pets.conditions = @conditions, " +
                               $" Pets.arrivalDate = @arrivalDate, " +
                               $" Pets.color = @color, " +
                               $" HealthStatus = @healthStatus " +
                               $"WHERE Pets.id = {item.Id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nickname", item.Nickname);
                cmd.Parameters.AddWithValue("@kindId", item.KindId);
                cmd.Parameters.AddWithValue("@breedId", item.BreedId);
                cmd.Parameters.AddWithValue("@conditions", item.Conditions);
                cmd.Parameters.AddWithValue("@arrivalDate", item.ArrivalDate);
                cmd.Parameters.AddWithValue("@color", item.Color);
                cmd.Parameters.AddWithValue("@healthStatus", item.HealthStatus);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"DELETE FROM Pets WHERE Pets.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
