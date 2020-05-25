using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectWarships_Tools.Cryptography;
using Warships_DAL.Repositories;

namespace ProjectWarships_Angular_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IRSAEncryption _encrypt;   
        protected IUser _userService;
        public BaseController(IRSAEncryption encrypt, IUser userService)
        {
            _encrypt = encrypt;       
            _userService = userService;
        }
    }
}