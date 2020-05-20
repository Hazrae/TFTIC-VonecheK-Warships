using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectWarships_Web.Infrastructure;
using ProjectWarships_Web.Utils;
using Vereyon.Web;

namespace ProjectWarships_Web.Controllers
{
    [LoggedIn]
    public class BaseController : Controller
    {
        protected internal IAPIConsume ConsumeInstance { get; set; }
        protected internal ISessionManager SessionManager { get; set; }

        protected internal IFlashMessage FlashMessage { get; set; }


        protected BaseController(IAPIConsume _apiConsume, ISessionManager _session, IFlashMessage flashMessage)
        {         
            ConsumeInstance = _apiConsume;
            SessionManager = _session;
            FlashMessage = flashMessage;
        }
    }
}