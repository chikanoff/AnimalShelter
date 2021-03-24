using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Services
{
    public class AuthentificationService
    {
        public static int Id { get; set; }

        public static void AuthUser(int id)
        {
            Id = id;
        }

    }
}
