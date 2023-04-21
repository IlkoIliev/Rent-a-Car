using API.Models.Roles;
using API.Models.Users;
using DATA;
using DATA.Entities;
using DATA.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticationManager = API.Models.AuthenticationManager;

namespace API.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserRepository _userRepository;

        public UsersController()
        {
            this._userRepository = new UserRepository(new RentACarDbContext());
        }

        // GET: Users
        [HttpGet]
        public ActionResult Index(UsersIndexVM model)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            if (AuthenticationManager.LoggedUser.Role.Name == "owner")
            {
                IQueryable<User> query = this._userRepository.GetAll();
                query = query.OrderBy(u => u.Id);

                model.Items = query.Select(u => new UserVM
                {
                    Id = u.Id,
                    Username = u.Username,
                    Firstname = u.Firstname,
                    Sirname = u.Sirname,
                    RoleName = u.Role.Name
                }).ToList();
            }

            if (AuthenticationManager.LoggedUser.Role.Name == "buyer")
            {
                var userId = AuthenticationManager.LoggedUser.Id;

                IQueryable<User> query = _userRepository.GetAll(u => u.Id == userId);

                model.Items = query.Select(u => new UserVM
                {
                    Id = u.Id,
                    Username = u.Username,
                    Firstname = u.Firstname,
                    Sirname = u.Sirname,
                    RoleName = u.Role.Name
                }).ToList();
            }

            return View(model);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            User user = this._userRepository.GetOne(id);

            UsersDetailsVM model = new UsersDetailsVM
            {
                Id = id,
                RoleName = user.Role.Name,
                Firstname = user.Firstname,
                Sirname = user.Sirname,
                Egn = user.Egn,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Username = user.Username
            };

            return View(model);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            UsersCreateVM usersCreateVM = new UsersCreateVM();

            RentACarDbContext context = new RentACarDbContext();
            usersCreateVM.Roles = context.Roles.Select(r => new RolesPair
            {
                Name = r.Name,
                Id = r.Id
            }).ToList();

            context.Dispose();

            return View(usersCreateVM);
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            User user = new User()
                {
                    Username = collection["Username"],
                    Password = collection["Password"],
                    Firstname = collection["Firstname"],
                    Sirname = collection["Sirname"],
                    Egn = collection["Egn"],
                    PhoneNumber = collection["PhoneNumber"],
                    Email = collection["Email"],
                    RoleId = Convert.ToInt32(collection["RoleId"]),
                };

                _userRepository.Add(user);

            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            UsersEditVM item;

            if (id == null)
            {
                item = new UsersEditVM();
            }
            else
            {
                User user = this._userRepository.GetOne(id.Value);

                item = new UsersEditVM
                {
                    Id = id.Value,
                    Firstname = user.Firstname,
                    Sirname = user.Sirname,
                    Username = user.Username,
                    Password = user.Password,
                    RoleId = user.RoleId,
                };
            }

            RentACarDbContext context = new RentACarDbContext();
            item.Roles = context.Roles.Select(r => new RolesPair
            {
                Name = r.Name,
                Id = r.Id
            }).ToList();

            context.Dispose();

            return View(item);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            User user = this._userRepository.GetOne(id);

            user.Firstname = collection["Firstname"];
            user.Sirname = collection["Sirname"];
            user.Username = collection["Username"];
            user.Password = collection["Password"];
            user.RoleId = Convert.ToInt32(collection["RoleId"]);

            this._userRepository.Update(user);

            return RedirectToAction("Index");

        }

        // GET: Users/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            User user = this._userRepository.GetOne(id);

            UsersDeleteVM model = new UsersDeleteVM
            {
                Id = user.Id,
                Username = user.Username,
                Firstname = user.Firstname,
                Sirname = user.Sirname,
                RoleName = user.Role.Name
            };

            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ViewBag.Firstname = collection["Firstname"];
            ViewBag.Sirname = collection["Sirname"];

            User user = this._userRepository.GetOne(id);
            this._userRepository.Remove(user);

            return RedirectToAction("Index");
        }
    }
}
