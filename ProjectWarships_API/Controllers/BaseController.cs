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
    public class BaseController : ControllerBase
    {
        protected IRSAEncryption _encrypt;
        public BaseController(IRSAEncryption encrypt)
        {
            _encrypt = encrypt;
        }
    }
}