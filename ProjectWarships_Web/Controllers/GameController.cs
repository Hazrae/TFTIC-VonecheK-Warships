using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWarships_Web.Infrastructure;

namespace ProjectWarships_Web.Controllers
{
    public class GameController : Controller
    {       
        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Battle()
        {
            return View();
        }
    }
}