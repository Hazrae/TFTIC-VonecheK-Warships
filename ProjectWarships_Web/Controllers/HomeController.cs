using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectWarships_Web.Infrastructure;
using ProjectWarships_Web.Models;
using ProjectWarships_Web.Utils;
using Vereyon.Web;

namespace ProjectWarships_Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> Logger;
        public HomeController(ILogger<HomeController> logger, IAPIConsume _consumeInstance, ISessionManager _session, IFlashMessage flash) : base(_consumeInstance, _session, flash)
        {
            Logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
