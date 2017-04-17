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