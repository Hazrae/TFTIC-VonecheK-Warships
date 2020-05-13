using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWarships_API.Models;
using ProjectWarships_API.Utils;
using Warships_DAL.Utils;

namespace ProjectWarships_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Handler.UserServiceInstance.GetAll().Select(x=>x.toAPI());
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public User Get(int id)
        {
            return Handler.UserServiceInstance.GetOne(id).toAPI();
        }

        // POST: api/User
        [HttpPost]
        public void Post(User u)
        {
            Handler.UserServiceInstance.Create(u.toDAL());
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put( User u)
        {
            Handler.UserServiceInstance.Update(u.toDAL());
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Handler.UserServiceInstance.Delete(id);
        }

        [HttpPost]
        [Route("api/User/CheckMail")]
        public bool CheckMail(string mail)
        {
            return Handler.UserServiceInstance.CheckMail(mail);
        }

        [HttpPost]
        [Route("api/User/CheckLogin")]
        public bool CheckLogin(string login)
        {
            return Handler.UserServiceInstance.CheckLogin(login);
        }
    }
}
