using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Warships_DAL.Utils
{
    public static class Handler
    {
        private const string stringConnec = @"Data Source=DESKTOP-7ND5R6T;Initial Catalog=WarshipsDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static SqlConnection _connecDB;

        public static SqlConnection ConnecDB
        {
            get
            {
                _connecDB = _connecDB ?? new SqlConnection(stringConnec);
                return _connecDB;
            }

        }

    }

}
