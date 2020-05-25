using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectWarships_Angular_API.Models;
using ProjectWarships_Angular_API.Utils;
using ProjectWarships_Tools.Cryptography;
using Warships_DAL.Repositories;

namespace ProjectWarships_Angular_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {          
           

        public UserController(IRSAEncryption encrypt, IUser userService) : base(encrypt, userService) { }
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetAll().Select(x => x.toAPI());
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userService.GetOne(id).toAPI();
        }

        // POST: api/User
        [HttpPost]
        public UserResponse Post(User u)
        {
            try
            {
                string pwDecrypt = _encrypt.Decrypt(Convert.FromBase64String(u.Password));
                u.Password = pwDecrypt;
                _userService.Create(u.toDAL());
            }
            catch (SqlException e)
            {
                switch (e.Number)
                {
                    case 2627:
                        {
                            if (e.Message.Contains("mail"))
                                return new UserResponse { ErrorCode = 1 };
                            else
                                return new UserResponse { ErrorCode = 2 };
                        }
                }
            }
            return new UserResponse();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public UserResponse Put(int id, User u)
        {
            try
            {
                _userService.Update(id, u.toDAL()); ;
            }
            catch (SqlException e)
            {
                switch (e.Number)
                {
                    case 2627:
                        {
                            if (e.Message.Contains("mail"))
                                return new UserResponse { ErrorCode = 1 };
                            else
                                return new UserResponse { ErrorCode = 2 };
                        }
                }
            }
            return new UserResponse();
        }

        // PUT: api/User/PutPw/5
        [HttpPut]
        [Route("ChangePw/{id}")]
        public UserResponse ChangePw(int id, UserPassword u)
        {
            string pwDecrypt = _encrypt.Decrypt(Convert.FromBase64String(u.Password));
            u.Password = pwDecrypt;
            pwDecrypt = _encrypt.Decrypt(Convert.FromBase64String(u.OldPassword));
            u.OldPassword = pwDecrypt;

            int state = _userService.UpdatePassword(id, u.toDALPW()); ;
            if (state == 1)
                return new UserResponse { ErrorCode = 3 };
            else
                return new UserResponse();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.Delete(id);
        }

        [HttpPost]   
        [Route("Login")]
        public User Login(LogUser u)
        {
            string pwDecrypt = _encrypt.Decrypt(Convert.FromBase64String(u.Password));
            u.Password = pwDecrypt;
            return _userService.Login(u.Login,u.Password).toAPI();
        }

    }
}
