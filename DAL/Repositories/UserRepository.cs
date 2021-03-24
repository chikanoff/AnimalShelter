using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DAL.Connection;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private DbConnection _db;
        public UserRepository()
        {
            _db = new DbConnection();
        }
        public User Get(int id)
        {
            User user = new User();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Users WHERE Users.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            UserTypeId = reader.GetInt32(3),
                            FullName = reader.GetString(4),
                            Address = reader.GetString(5),
                            PhoneNumber = reader.GetString(6)
                        };
                    }
                }
            }
            return user;
        }

        public IList<User> GetAll()
        {
            IList<User> users = new List<User>();

            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            UserTypeId = reader.GetInt32(3),
                            FullName = reader.GetString(4),
                            Address = reader.GetString(5),
                            PhoneNumber = reader.GetString(6)
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public void Create(User item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = "INSERT INTO Users(username, password, userTypeId, fullName, address, phoneNumber) VALUES(@username, @password, @userTypeId, @fullName, @address, @phoneNumber)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@password", item.Password);
                cmd.Parameters.AddWithValue("@userTypeId", item.UserTypeId);
                cmd.Parameters.AddWithValue("@fullName", item.FullName);
                cmd.Parameters.AddWithValue("@address", item.Address);
                cmd.Parameters.AddWithValue("@phoneNumber", item.PhoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(User item)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"UPDATE Users " +
                               $"SET Users.username = @username, " +
                               $"Users.password = @password" +
                               $", Users.userTypeId = @userTypeId " +
                               $", Users.fullName = @fullName " +
                               $", Users.address = @address " +
                               $", Users.phoneNumber = @phoneNumber " +
                               $"WHERE Users.id = {item.Id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@password", item.Password);
                cmd.Parameters.AddWithValue("@userTypeId", item.UserTypeId);
                cmd.Parameters.AddWithValue("@fullName", item.FullName);
                cmd.Parameters.AddWithValue("@address", item.Address);
                cmd.Parameters.AddWithValue("@phoneNumber", item.PhoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"DELETE FROM Users WHERE Users.id = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public int? GetUserTypeId(string username, string password)
        {
            int? id = null;
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Users WHERE Users.username = @username AND Users.password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(3);
                    }
                }
            }

            return id;
        }

        public bool LoginUser(string username, string password)
        {
            bool login = false;
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Users WHERE Users.username = @username AND Users.password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    login = true;
                }
            }

            return login;
        }

        public User GetUser(string username, string password)
        {
            User user = new User();
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Users WHERE Users.username = @username AND Users.password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            UserTypeId = reader.GetInt32(3),
                            FullName = reader.GetString(4),
                            Address = reader.GetString(5),
                            PhoneNumber = reader.GetString(6)
                        };
                    }
                }
            }

            return user;
        }

        public bool CheckUsername(string username)
        {
            bool checker = false;
            using (SqlConnection conn = _db.ConnectToDatabase())
            {
                conn.Open();
                string query = $"SELECT * FROM Users WHERE Users.username = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    checker = true;
                }
            }

            return checker;
        }
    }
}
