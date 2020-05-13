using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectWarships_Web.Models;
using ProjectWarships_Web.Utils;

namespace ProjectWarships_Web.Controllers
{
    public class UserController : BaseController
    {
        // GET: User   
        public UserController(IAPIConsume consumeInstance) : base(consumeInstance){        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterUser ru)
        {
            try
            {
                
                    //List<User> listUser = _consumeInstance.Get<List<User>>("User");

                if (_consumeInstance.PostWithReturn<string,bool>("User/CheckMail",ru.Mail))
                    {                      
                        ModelState.AddModelError(string.Empty, "Email address already in use");
                    }
                   
                    if (_consumeInstance.PostWithReturn<string, bool>("User/CheckLogin", ru.Mail))
                    {                            
                       ModelState.AddModelError(string.Empty, "Login already in use");                
                    }                  
                           
                    
                    
                 if (ModelState.IsValid)
                {
                    _consumeInstance.Post<RegisterUser>("User", ru);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return View(ru);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}