using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL = Warships_DAL.Models;
using API = ProjectWarships_API.Models;
using System.Security.Cryptography;

namespace ProjectWarships_API.Utils
{
    public static class Mappers
    {

        public static DAL.User toDAL(this API.User u)
        {
            return new DAL.User
            {
                Id = u.Id,
                Login = u.Login,
                Mail = u.Mail,
                Password = u.Password,
                BirthDate = u.BirthDate,      
                IsDelete = u.IsDelete,
                isActive = u.IsActive,
                IsAdmin = u.IsAdmin
            };   
        }

        public static API.User toAPI(this DAL.User u)
        {
            return new API.User
            {
                Id = u.Id,
                Login = u.Login,
                Mail = u.Mail,
                Password = u.Password,
                BirthDate = u.BirthDate,            
                IsDelete = u.IsDelete,
                IsActive = u.isActive,
                IsAdmin = u.IsAdmin
            };
        }

        public static DAL.UserPassword toDALPW(this API.UserPassword u)
        {
            return new DAL.UserPassword
            {                
                Password = u.Password,
                OldPassword = u.OldPassword,         
            };
        }
    }
}
