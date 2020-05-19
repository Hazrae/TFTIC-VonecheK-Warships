using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectWarships_Tools.Cryptography;
using Warships_DAL.Repositories;

namespace ProjectWarships_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IRSAEncryption encrypt, IUser userService) : base(encrypt, userService) { }

        [Route("GetKey")]
        public byte[] GetKey()
        {

            return _encrypt.PublicBinaryKey;
        }
    }
}