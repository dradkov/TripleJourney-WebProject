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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                var db = new BlogDbContext();

                var startDate = new DateTime(car.PickUpDate.Year, car.PickUpDate.Month, car.PickUpDate.Day,car.PickUpTime.Hour,car.PickUpTime.Minute,0);
                var endDate = new DateTime(car.DropOffDate.Year,car.DropOffDate.Month,car.DropOffDate.Day,car.DropOffTime.Hour,car.DropOffTime.Minute,0);
                
                var span = endDate.Subtract(startDate);
                var hours = span.TotalHours;
                decimal money = (decimal)hours * 7;

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
    }
}