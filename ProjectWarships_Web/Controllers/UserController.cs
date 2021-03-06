﻿using System;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ProjectWarships_Tools.Cryptography;
using ProjectWarships_Web.Infrastructure;
using ProjectWarships_Web.Models;
using ProjectWarships_Web.Utils;
using Vereyon.Web;

namespace ProjectWarships_Web.Controllers
{

    public class UserController : BaseController
    {

        private IRSAEncryption _encrypt;
        private IGoogleToken _googleToken;
        private IConfiguration _config;
        // GET: User   
        public UserController(IAPIConsume _consumeInstance, ISessionManager _session, IGoogleToken googleToken, IConfiguration config, IFlashMessage flash) : base(_consumeInstance, _session, flash) 
        {
            _googleToken = googleToken;
            _config = config;
        }
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
                        FlashMessage.Warning("Email already in use");
                        return View(ru);
                    }
                    else if (ur.ErrorCode == 2)
                    {
                        FlashMessage.Warning("Login already in use");
                        return View(ru);
                    }
                    else
                        FlashMessage.Confirmation("Account created");
                        return RedirectToAction("Login");
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
                        FlashMessage.Warning("Email already in use");
                        return View(user);
                    }
                    else if (ur.ErrorCode == 2)
                    {
                        FlashMessage.Warning("Login already in use");
                        return View(user);
                    }
                    else
                        SessionManager.Login = user.Login;
                    FlashMessage.Confirmation("Profile updated with success");
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

                    UserResponse ur = ConsumeInstance.PutWithReturn<EditPassword, UserResponse>("User/ChangePw/" + SessionManager.Id, user);
                    if (ur.ErrorCode == 3)
                    {
                        FlashMessage.Warning("The old password doesn't match");
                        return View(user);
                    }
                    else
                        FlashMessage.Confirmation("Password Changed, Please reconnect");
                        SessionManager.Abandon();
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
                        FlashMessage.Warning("This account doesn't exists");
                        return View(lu);
                    }
                    else if (u.IsActive == false)
                    {
                        FlashMessage.Warning("Your account has been deactivate, Please contact the admin");
                        return RedirectToAction("Contact");
                    }
                    else
                    {
                        SessionManager.Id = u.Id;
                        SessionManager.Login = u.Login;
                        FlashMessage.Confirmation("Welcome " + u.Login);
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

        public IActionResult Contact()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {        
            SaslMechanismOAuth2 oauth2 = _googleToken.Token().Result;
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Contact", "from.website@example.com"));
            message.To.Add(new MailboxAddress(_config.GetValue<string>("Google:Mail")));
            message.Subject = $"[Contact from your website] { contact.Subject }";

            BodyBuilder builder = new BodyBuilder
            {
                HtmlBody = $"<div><span style='font-weight: bold'>De</span> : {contact.Name} </div><div><span style='font-weight: bold'>Mail</span> : {contact.Email}</div><div style='margin-top: 30px'>{contact.Message}</div>"
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                // use the OAuth2.0 access token obtained above          
                client.Authenticate(oauth2);

                client.Send(message);
                client.Disconnect(true);
                FlashMessage.Confirmation("Mail sent with success");
                ModelState.Clear();
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