using BlogTriple.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogTriple.Controllers
{
    public class HotelsController : Controller
    {

        [HttpGet]
        [Authorize]
        public ActionResult Destination()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Destination(HotelsViewModel city)
        {

            if (ModelState.IsValid)
            {
                var database = new BlogDbContext();

                var startDate = new DateTime(city.From.Year, city.From.Month, city.From.Day);
                var endDate = new DateTime(city.To.Year, city.To.Month, city.To.Day);

                var numDays = endDate.Subtract(startDate);
                var convertDays = numDays.TotalDays;

                decimal result = 0;
                if (city.Rooms == "1")
                {
                    result = (decimal)convertDays * 20;
                }
                else if (city.Rooms == "2")
                {
                    result = (decimal)convertDays * 30;
                }
                else if (city.Rooms == "3")
                {
                    result = (decimal)convertDays * 40;
                }
                else if (city.Rooms == "4")
                {
                    result = (decimal)convertDays * 60;
                }

                city.Price = result;
                database.Destinations.Add(city);
                database.SaveChanges();

                return RedirectToAction("DestinationDetails", new { id = city.Id });
            }
            return View();
        }


        public ActionResult DestinationDetails(int? id)
        {
            var database = new BlogDbContext();

            var city = database.Destinations.Where(c => c.Id == id).FirstOrDefault();

            if (city == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(city);
        }



    }
}