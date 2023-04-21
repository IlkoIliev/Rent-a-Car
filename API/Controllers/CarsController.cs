using API.Models.Cars;
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
    public class CarsController : Controller
    {
        private readonly CarRepository _carRepository;

        public CarsController()
        {
            this._carRepository = new CarRepository(new RentACarDbContext());
        }

        // GET: Cars
        public ActionResult Index(CarsIndexVM model)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            IQueryable<Car> query = this._carRepository.GetAll();
                query = query.OrderBy(u => u.Id);

                model.Items = query.Select(c => new CarVM
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Seats = c.Seats,
                    Price = c.Price,
                    Description = c.Description
                }).ToList();

            return View(model);
        }

        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            Car car = this._carRepository.GetOne(id);

            CarsDetailsVM model = new CarsDetailsVM
            {
                Id = id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Seats = car.Seats,
                Price = car.Price,
                Description = car.Description
            };

            return View(model);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        public ActionResult Create(CarsCreateVM carsCreateVMModel)
        {
            if (this.ModelState.IsValid)
            {
                Car car = new Car()
                {
                    Brand = carsCreateVMModel.Brand,
                    Model = carsCreateVMModel.Model,
                    Year = carsCreateVMModel.Year,
                    Seats = carsCreateVMModel.Seats,
                    Price = carsCreateVMModel.Price,
                    Description = carsCreateVMModel.Description
                };

                _carRepository.Add(car);

                TempData["AlertMessage"] = "Success created " + car.Brand + " " + car.Model;
            }

            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError("registerFailed", "Something went wrong!");
                return View(carsCreateVMModel);
            }

            return RedirectToAction("Index", "Cars");
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            Car car = this._carRepository.GetOne(id);

            CarsEditVM model = new CarsEditVM
            {
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Seats = car.Seats,
                Price = car.Price,
                Description = car.Description
            };

            return View(model);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CarsEditVM carsEditVMModel)
        {
            if (this.ModelState.IsValid)
            {
                ViewBag.Brand = carsEditVMModel.Brand;
                ViewBag.Model = carsEditVMModel.Model;
                ViewBag.Message = "Success updated";

                Car car = new Car()
                {
                    Id = id,
                    Brand = carsEditVMModel.Brand,
                    Model = carsEditVMModel.Model,
                    Year = carsEditVMModel.Year,
                    Seats = carsEditVMModel.Seats,
                    Price = carsEditVMModel.Price,
                    Description = carsEditVMModel.Description
                };

                _carRepository.Update(car);

                TempData["AlertMessage"] = "Success updated " + car.Brand + car.Model;
            }

            if (!this.ModelState.IsValid)
            {
                ModelState.AddModelError("registerFailed", "Something went wrong!");
                return View(carsEditVMModel);
            }

            return RedirectToAction("Index", "Cars");
        }

        // GET: Cars/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Car car = this._carRepository.GetOne(id);

            CarsDeleteVM model = new CarsDeleteVM
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Seats = car.Seats,
                Price = car.Price,
                Description = car.Description
            };

            return View(model);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = this._carRepository.GetOne(id);
            this._carRepository.Remove(car);

            TempData["AlertMessage"] = "Success deleted " + car.Brand + car.Model;

            return RedirectToAction("Index");
        }
    }
}
