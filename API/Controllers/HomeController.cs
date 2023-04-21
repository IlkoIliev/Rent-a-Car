using API.Models;
using API.Models.Home;
using DATA;
using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        private readonly RentACarDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new RentACarDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Login());
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (this.ModelState.IsValid)
            {
                AuthenticationManager.Authenticate(model.Username, model.Password);

                if (AuthenticationManager.LoggedUser == null)
                    ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
            }

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View(new Register());
        }

        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (this.ModelState.IsValid)
            {
                User user = new User()
                {
                    Username = model.Username,
                    Password = model.Password,
                    Firstname = model.Firstname,
                    Sirname = model.Sirname,
                    Egn = model.Egn,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    RoleId = 2,
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }

            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError("registerFailed", "Something went wrong!");
                return View(model);
            }

            return RedirectToAction("Login", "Home");
        }
    }
}