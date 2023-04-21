using API.Models.Rents;
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
    public class RentsController : Controller
    {
        private readonly RentRepository _rentRepository;

        public RentsController()
        {
            this._rentRepository = new RentRepository(new RentACarDbContext());
        }

        // GET: Rents
        public ActionResult Index(RentsIndexVM rentsIndexVM)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            if (AuthenticationManager.LoggedUser.Role.Name == "owner")
            {
                IQueryable<Rent> query = this._rentRepository.GetAll();
                query = query.OrderBy(r => r.Id);

                rentsIndexVM.Items = query.Select(r => new RentsAllVM
                {
                    Id = r.Id,
                    CarId = r.CarId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    UserId = r.UserId,
                    State = r.State
                }).ToList();
            }

            if (AuthenticationManager.LoggedUser.Role.Name == "buyer")
            {
                var userId = AuthenticationManager.LoggedUser.Id;

                IQueryable<Rent> query = _rentRepository.GetAll(r => r.UserId == userId);

                rentsIndexVM.Items = query.Select(r => new RentsAllVM
                {
                    Id = r.Id,
                    CarId = r.CarId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    UserId = r.UserId,
                    State = r.State
                }).ToList();
            }

            return View(rentsIndexVM);
        }

        // GET: Rents/Details/5
        public ActionResult Details(int id)
        {
            List<Rent> rent = this._rentRepository.GetOneRentDetails(id);

            RentsDetailsVM model = new RentsDetailsVM
            {
                Id = rent[0].Id,
                Username = rent[0].User.Username,
                Firstname = rent[0].User.Firstname,
                Sirname = rent[0].User.Sirname,
                Egn = rent[0].User.Egn,
                PhoneNumber = rent[0].User.PhoneNumber,
                Email = rent[0].User.Email,
                Brand = rent[0].Car.Brand,
                Model = rent[0].Car.Model,
                Year = rent[0].Car.Year,
                Seats = rent[0].Car.Seats,
                Price = rent[0].Car.Price,
                Description = rent[0].Car.Description,
                StartDate = rent[0].StartDate,
                EndDate = rent[0].EndDate,
                State = rent[0].State
            };

            return View(model);
        }

        // GET: Rents/Create
        public ActionResult Create(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            var userId = AuthenticationManager.LoggedUser.Id;

            CarRepository _carRepository = new CarRepository(new RentACarDbContext());
            Car car = _carRepository.GetOne(id);

            RentsCreateVM model = new RentsCreateVM
            {
                CarId = car.Id,
                StartDate = DateTime.Now,
                UserId = userId,
                Firstname = AuthenticationManager.LoggedUser.Firstname,
                Sirname = AuthenticationManager.LoggedUser.Sirname,
                Car = car
            };

            return View(model);
        }

        // POST: Rents/Create
        [HttpPost]
        public ActionResult Create(RentsCreateVM rentsCreateVM)
        {
            if (this.ModelState.IsValid)
            {
                Rent rent = new Rent()
                {
                    CarId = rentsCreateVM.CarId,
                    StartDate = rentsCreateVM.StartDate,
                    EndDate = rentsCreateVM.EndDate,
                    UserId = rentsCreateVM.UserId
                };

                _rentRepository.Add(rent);

                TempData["AlertMessage"] = "Success created rent from " + rent.StartDate + " to " + rent.EndDate;
            }

            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError("registerFailed", "Something went wrong!");
                return View(rentsCreateVM);
            }

            return RedirectToAction("Index", "Rents");
        }

        // GET: Rents/Edit/5
        public ActionResult Edit(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            Rent rent = this._rentRepository.GetOne(id);

            RentsEditVM model = new RentsEditVM
            {
                Id = rent.Id,
                CarId = rent.CarId,
                StartDate = rent.StartDate,
                EndDate = rent.EndDate,
                UserId = rent.UserId,
                State = rent.State
            };

            return View(model);
        }

        // POST: Rents/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, RentsEditVM rentsEditVMModel, FormCollection collection)
        {
            if (this.ModelState.IsValid)
            {
                Rent rent = new Rent()
                {
                    Id = id,
                    CarId = rentsEditVMModel.CarId,
                    StartDate = rentsEditVMModel.StartDate,
                    EndDate = rentsEditVMModel.EndDate,
                    UserId = rentsEditVMModel.UserId,
                    State = Convert.ToByte(collection["State"]),//((byte)rentsEditVMModel.States)
                };

                _rentRepository.Update(rent);

                TempData["AlertMessage"] = "Success updated rent " + rentsEditVMModel.Id;
            }

            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError("registerFailed", "Something went wrong!");
                return View(rentsEditVMModel);
            }

            return RedirectToAction("Index", "Rents");
        }

        // GET: Rents/Delete/5
        public ActionResult Delete(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            Rent rent = this._rentRepository.GetOne(id);

            RentsDeleteVM model = new RentsDeleteVM
            {
                Id = rent.Id,
                CarId = rent.CarId,
                StartDate = rent.StartDate,
                EndDate = rent.EndDate,
                UserId = rent.UserId
            };

            return View(model);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Rent rent = this._rentRepository.GetOne(id);
            this._rentRepository.Remove(rent);

            TempData["AlertMessage"] = "Success deleted " + rent.Id;

            return RedirectToAction("Index");
        }
        /*
        public ActionResult Rents()
        {
            List<Rent> rents =    this._rentRepository.GetAllRent(2);

            return RedirectToAction("Index");
        }*/
    }
}
