using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Warships_DAL.Services;

namespace Warships_DAL.Utils
{
    public static class Handler
    {
        private static UserService _userServiceInstance;

        public static UserService UserServiceInstance
        {
            get
            {
                _userServiceInstance = _userServiceInstance ?? new UserService();
                return _userServiceInstance;
            }
        }

    }

}
