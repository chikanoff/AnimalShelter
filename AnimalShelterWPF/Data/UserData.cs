using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace GUI.Data
{
    public class UserData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public UserTypeData UserType { get; set; }

    }
}
