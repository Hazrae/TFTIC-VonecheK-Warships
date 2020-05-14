using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWarships_Tools.Cryptography;

namespace ProjectWarships_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        public AuthController(IRSAEncryption encrypt) : base(encrypt) { }

        [Route("GetKey")]
        public byte[] GetKey()
        {

            return _encrypt.PublicBinaryKey;
        }
    }
}