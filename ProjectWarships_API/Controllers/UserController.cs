using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWarships_API.Models;
using ProjectWarships_API.Utils;
using ProjectWarships_Tools.Cryptography;
using Warships_DAL.Utils;

namespace ProjectWarships_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IRSAEncryption encrypt) : base(encrypt) { }
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Handler.UserServiceInstance.GetAll().Select(x => x.toAPI());
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public User Get(int id)
        {
            return Handler.UserServiceInstance.GetOne(id).toAPI();
        }

        // POST: api/User
        [HttpPost]
        public UserResponse Post(User u)
        {
            try
            {
                string pwDecrypt = _encrypt.Decrypt(Convert.FromBase64String(u.Password));
                u.Password = pwDecrypt;
                Handler.UserServiceInstance.Create(u.toDAL());
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
        public void Put(User u)
        {
            Handler.UserServiceInstance.Update(u.toDAL());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Handler.UserServiceInstance.Delete(id);
        }

    }
}
