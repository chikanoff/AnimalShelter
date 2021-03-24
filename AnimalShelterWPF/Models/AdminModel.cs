using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DAL.Entities;
using GUI.Data;

namespace GUI.Models
{
    public class AdminModel
    {
        public ObservableCollection<UserData> GetUsers()
        {
            return new ObservableCollection<UserData>(Mapping.GetUsers());
        }

        public void AddUser(UserData user)
        {
            Mapping.AddUser(user);
        }

        public void EditUser(UserData user)
        {
            Mapping.EditUser(user);
        }

        public void DeleteUser(UserData user)
        {
            Mapping.DeleteUser(new User
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                UserTypeId = user.UserType.Id,
                FullName = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            });
        }

        public ObservableCollection<UserTypeData> GetUserTypes()
        {
            return new ObservableCollection<UserTypeData>(Mapping.GetUserTypes());
        }
    }
}
