using E_CommerceProject1.DAL;
using E_CommerceProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceProject1.Controllers
{
    public class UserController : Controller
    {
       // UserDAL db = new UserDAL();
        private readonly IConfiguration configuration;
        UserDAL userdal;
        public UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
            userdal = new UserDAL(configuration);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                int res = userdal.UserRegister(user);
                if (res == 1)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User u)
        {
            User user = userdal.UserLogin(u);
            if (user.Password == user.Password)
            {
                HttpContext.Session.SetString("username", user.Name + " " + user.Email);
                HttpContext.Session.SetString("userid", user.Id.ToString());
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
         
    }
}

