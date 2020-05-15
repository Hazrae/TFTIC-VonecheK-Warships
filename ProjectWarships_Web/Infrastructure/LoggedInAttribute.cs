using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWarships_Web.Controllers;

namespace ProjectWarships_Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class LoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            BaseController controller = (BaseController)context.Controller;
            controller.ViewBag.isLogged = (controller.SessionManager.Id != -1);
        }
    }
}
