using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Connection;

namespace DAL.Repositories
{
    public class OperationRepository : IRepository<Operation>
    {
        private DbConnection _db;
        public OperationRepository()
        {
            _db = new DbConnection();
        }

        public void Create(Operation item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "INSERT INTO Operations(petId, operationDate, userId) VALUES(@petId, @operationDate, @userId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@petId", item.PetId);
                cmd.Parameters.AddWithValue("@operationDate", item.OperationDate);
                cmd.Parameters.AddWithValue("@userId", item.UserId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"DELETE * FROM Operations WHERE Operations.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public Operation Get(int id)
        {
            Operation operation = new Operation();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Operations WHERE Operations.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        operation = new Operation
                        {
                            Id = reader.GetInt32(0),
                            PetId = reader.GetInt32(1),
                            OperationDate = reader.GetDateTime(2),
                            UserId = reader.GetInt32(3)
                        };
                    }
                }
            }
            return operation;
        }

        public IList<Operation> GetAll()
        {
            IList<Operation> operations = new List<Operation>();
                
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Operations";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Operation operation = new Operation
                        {
                            Id = reader.GetInt32(0),
                            PetId = reader.GetInt32(1),
                            OperationDate = reader.GetDateTime(2),
                            UserId = reader.GetInt32(3)
                        };
                        operations.Add(operation);
                    }
                }
            }
            return operations;
        }

        public void Update(Operation item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"UPDATE Operations SET petId = @petId SET operationDate = @operationDate SET userId = @userId WHERE Operations.id = {item.Id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@petId", item.PetId);
                cmd.Parameters.AddWithValue("@operationDate", item.OperationDate);
                cmd.Parameters.AddWithValue("@userId", item.UserId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
