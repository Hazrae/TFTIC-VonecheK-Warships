using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWarships_Web.Utils;

namespace ProjectWarships_Web.Controllers
{
    public class BaseController : Controller
    {
        protected IAPIConsume _consumeInstance { get; set; }

        protected BaseController(IAPIConsume APIConsume)
        {
            _consumeInstance = APIConsume;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}