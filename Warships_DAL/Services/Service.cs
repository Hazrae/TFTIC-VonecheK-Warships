using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Warships_DAL.Services
{
    public class Service
    {   
        protected IConfiguration _config;

        public string stringConnec
        {
            get { return _config.GetConnectionString("WarshipsDB"); }
        }

        protected Service(IConfiguration config)
        {
            _config = config;

        }     
       
    }
}
