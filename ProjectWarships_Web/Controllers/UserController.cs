using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectWarships_Tools.Cryptography;
using ProjectWarships_Web.Models;
using ProjectWarships_Web.Utils;

namespace ProjectWarships_Web.Controllers
{
    public class UserController : BaseController
    {
        private IRSAEncryption _encrypt;
        // GET: User   
        public UserController(IAPIConsume consumeInstance) : base(consumeInstance) { }
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

                if (ModelState.IsValid)
                {
                    byte[] pwEncrypt;
                    _encrypt = new RSAEncryption(_consumeInstance.Get<byte[]>("Auth/GetKey"));
                    pwEncrypt = _encrypt.Encrypt(ru.Password);
                    ru.Password = Convert.ToBase64String(pwEncrypt);

                    UserResponse ur = _consumeInstance.PostWithReturn<RegisterUser, UserResponse>("User", ru);
                    if (ur.ErrorCode == 1)
                    {
                        ModelState.AddModelError(string.Empty, "E-mail adress is already in use");
                        return View(ru);
                    }
                    else if (ur.ErrorCode == 2)
                    {
                        ModelState.AddModelError(string.Empty, "Login is already in use");
                        return View(ru);
                    }
                    else
                        return RedirectToAction("Index", "Home");
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