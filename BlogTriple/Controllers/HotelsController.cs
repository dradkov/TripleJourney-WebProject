using BlogTriple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogTriple.Controllers
{
    public class HotelsController : Controller
    {
        // GET: Hotels
        public ActionResult Index()
        {
            return View();
        }

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
            var database = new BlogDbContext();

            if (ModelState.IsValid)
            {
                database.Destinations.Add(city);
                database.SaveChanges();

                return RedirectToAction("Details", "Hotels");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Details()
        {
            return View();
        }



    }
}