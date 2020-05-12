using System;
using System.Collections.Generic;
using System.Text;

namespace Warships_DAL.Models
{
    public class User
    {
        public int id;
        public string login;
        public string password;
        public string mail;
        public DateTime birthDate;
        public string country;
        public Boolean isDelete;
        public Boolean isActive;
        public Boolean isAdmin;
    }
}
