using System;
using System.Collections.Generic;
using System.Text;

namespace Warships_DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public DateTime BirthDate { get; set; }       
        public Boolean IsDelete { get; set; }
        public Boolean isActive { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}
