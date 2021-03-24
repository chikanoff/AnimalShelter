using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Repositories;
using GUI.Services;

namespace GUI.Models
{
    public class LoginModel
    {
        private UserRepository userRep;
        private UserTypeRepository userTypeRep;

        public LoginModel()
        {
            userRep = new UserRepository();
            userTypeRep = new UserTypeRepository();
        }

        public bool IsLogin(string username, string password)
        {
            return userRep.LoginUser(username, password);
        }

        public string GetUserType(string username, string password)
        {
            int? id = userRep.GetUserTypeId(username, password);
            if (id != null)
            {
                return userTypeRep.Get(id.Value).Name;
            }

            return null;
        }

        public void LoginUser(string username, string password)
        {
            User user = userRep.GetUser(username, password);
            AuthentificationService.AuthUser(user.Id);
        }
    }
}
