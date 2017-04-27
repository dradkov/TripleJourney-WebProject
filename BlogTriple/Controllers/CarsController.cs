using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTriple.Models;

namespace BlogTriple.Controllers
{
    public class CarsController : Controller
    {
        public ActionResult All()
        {
            var db = new BlogDbContext();

            var cars = db.RentCars.Select(c => new CarListingModel()
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                Make = c.Make,
                Model = c.ModelCar,
                Year = c.Year,
                PricePerDay = c.PricePerDay
            }).ToList();

            return View(cars);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                var db = new BlogDbContext();

                var startDate = new DateTime(car.PickUpDate.Year, car.PickUpDate.Month, car.PickUpDate.Day,car.PickUpTime.Hour,car.PickUpTime.Minute,0);
                var endDate = new DateTime(car.DropOffDate.Year,car.DropOffDate.Month,car.DropOffDate.Day,car.DropOffTime.Hour,car.DropOffTime.Minute,0);
                
                var span = endDate.Subtract(startDate);
                var hours = span.TotalHours;
                decimal money = (decimal)hours * 6;

                var userId = db.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First().Id;
                car.UserId = userId;
                car.Price = money;
                db.Cars.Add(car);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = car.Id });
            }

            return View(car);
        }

        public ActionResult Details(int id)
        {
            var db = new BlogDbContext();

            var car = db.Cars.Where(c => c.Id == id).FirstOrDefault();

            if (car == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(car);
        }

        public ActionResult CarDetails(int id)
        {
            var db = new BlogDbContext();

            var car = db.RentCars.Where(c => c.Id == id).Select(c => new CarDetailsModel
            {
                ModelCar = c.ModelCar,
                Power = c.Power,
                Year = c.Year,
                ImageUrl = c.ImageUrl,
                Make = c.Make,
                Color = c.Color,
                PricePerDay = c.PricePerDay,
            }).FirstOrDefault();

            if (car == null)
            {
               return HttpNotFound();
            }

            return View(car);
        }
    }
}