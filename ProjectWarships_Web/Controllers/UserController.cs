﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using ProjectWarships_Tools.Cryptography;
using ProjectWarships_Web.Infrastructure;
using ProjectWarships_Web.Models;
using ProjectWarships_Web.Utils;

namespace ProjectWarships_Web.Controllers
{

    public class UserController : BaseController
    {

        private IRSAEncryption _encrypt;
        // GET: User   
        public UserController(IAPIConsume _consumeInstance, ISessionManager _session) : base(_consumeInstance, _session) { }
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        [AuthRequired]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        [AnonymousRequired]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [AnonymousRequired]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterUser ru)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    byte[] pwEncrypt;
                    _encrypt = new RSAEncryption(ConsumeInstance.Get<byte[]>("Auth/GetKey"));
                    pwEncrypt = _encrypt.Encrypt(ru.Password);
                    ru.Password = Convert.ToBase64String(pwEncrypt);

                    UserResponse ur = ConsumeInstance.PostWithReturn<RegisterUser, UserResponse>("User", ru);
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
        [AuthRequired]
        public ActionResult Edit()
        {
            EditUser u = ConsumeInstance.Get<EditUser>("User/", SessionManager.Id);
            return View(u);
        }

        // POST: User/Edit/5
        [AuthRequired]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserResponse ur = ConsumeInstance.PutWithReturn<EditUser, UserResponse>("User/" + SessionManager.Id, user);
                    if (ur.ErrorCode == 1)
                    {
                        ModelState.AddModelError(string.Empty, "E-mail adress is already in use");
                        return View(user);
                    }
                    else if (ur.ErrorCode == 2)
                    {
                        ModelState.AddModelError(string.Empty, "Login is already in use");
                        return View(user);
                    }
                    else
                        SessionManager.Login = user.Login;
                    ViewBag.Confirm = "Profile updated with success";
                    return View(user);
                }
                else
                {
                    return View(user);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        [AuthRequired]
        public ActionResult EditPw()
        {
            return View();
        }

        // POST: User/Edit/5
        [AuthRequired]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPw(EditPassword user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] pwEncrypt;
                    _encrypt = new RSAEncryption(ConsumeInstance.Get<byte[]>("Auth/GetKey"));
                    pwEncrypt = _encrypt.Encrypt(user.Password);
                    user.Password = Convert.ToBase64String(pwEncrypt);
                    pwEncrypt = _encrypt.Encrypt(user.OldPassword);
                    user.OldPassword = Convert.ToBase64String(pwEncrypt);

                    UserResponse ur = ConsumeInstance.PutWithReturn<EditPassword, UserResponse>("User/PutPw/" + SessionManager.Id, user);
                    if (ur.ErrorCode == 3)
                    {
                        ModelState.AddModelError(string.Empty, "The old password doesn't match");
                        return View(user);
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(user);
                }
            }
            catch
            {
                return View();
            }
        }


        // GET: User/Delete/5
        [AuthRequired]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [AuthRequired]
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

        [AnonymousRequired]
        // GET: User/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Create
        [AnonymousRequired]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogUser lu)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    byte[] pwEncrypt;
                    _encrypt = new RSAEncryption(ConsumeInstance.Get<byte[]>("Auth/GetKey"));
                    pwEncrypt = _encrypt.Encrypt(lu.Password);
                    lu.Password = Convert.ToBase64String(pwEncrypt);
                    User u = ConsumeInstance.PostWithReturn<LogUser, User>("User/Login", lu);

                    if (u.Login != lu.Login)
                    {
                        ModelState.AddModelError(string.Empty, "This account doesn't exists");
                        return View(lu);
                    }
                    else
                    {
                        SessionManager.Id = u.Id;
                        SessionManager.Login = u.Login;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return View(lu);
                }
            }
            catch
            {
                return View();
            }
        }

        [AuthRequired]
        public ActionResult Logout()
        {
            SessionManager.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}