using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Connection;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserTypeRepository : IRepository<UserType>
    {
        private DbConnection _db;
        public UserTypeRepository()
        {
            _db = new DbConnection();
        }

        public UserType Get(int id)
        {
            UserType userType = new UserType();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM UserTypes WHERE UserTypes.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userType = new UserType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
            return userType;
        }

        public IList<UserType> GetAll()
        {
            IList<UserType> userTypes = new List<UserType>();

            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM UserTypes";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserType userType = new UserType
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        userTypes.Add(userType);
                    }
                }
            }
            return userTypes;
        }

        public void Create(UserType item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "INSERT INTO Operations VALUES(@name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(UserType item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"UPDATE Operations SET name = @name WHERE UserTypes.id = {item.Id}";
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
                string query = $"DELETE * FROM UserTypes WHERE UserTypes.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public int? GetUserTypeId()
        {
            int? managerTypeId = null;
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM UserTypes WHERE UserTypes.name = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", "User");

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        managerTypeId = reader.GetInt32(0);
                    }
                }
            }
            return managerTypeId;
        }
    }
}
