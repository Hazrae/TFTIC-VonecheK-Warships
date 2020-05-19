using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace Warships_DAL.Services
{
    public class Service
    {       

        //TODO : connection string dans un fichier de config
        public string stringConnec = @"Data Source=DESKTOP-7ND5R6T;Initial Catalog=WarshipsDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //public string stringConnec = ConfigurationManager.ConnectionStrings["WarshipsDB"].ConnectionString;
        protected Service()
        {          
        }
        
        
    }
}
