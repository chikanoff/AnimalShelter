using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Repositories;

namespace GUI.Models
{
    public class RegisterModel
    {
        private UserRepository userRep;
        private UserTypeRepository userTypeRep;

        public RegisterModel()
        {
            userRep = new UserRepository();
            userTypeRep = new UserTypeRepository();
        }
        public void CreateUser(string username, string password, string fullName, string address, string phoneNumber)
        {
            int? userTypeId = userTypeRep.GetUserTypeId();
            if (userTypeId != null)
            {
                userRep.Create(new User
                {
                    Username = username,
                    Password = password,
                    UserTypeId = userTypeId.Value,
                    FullName = fullName,
                    Address = address,
                    PhoneNumber = phoneNumber
                });
            }
            else
            {
                throw new Exception("add userType for user in db");
            }
        }
        public bool CheckUsername(string username)
        {
            return userRep.CheckUsername(username);
        }
    }
}
