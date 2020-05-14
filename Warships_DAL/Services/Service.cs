using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Warships_DAL.Services
{
    public class Service
    {       
        public string stringConnec = @"Data Source=DESKTOP-7ND5R6T;Initial Catalog=WarshipsDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        protected Service()
        {          
        }
        
        
    }
}
