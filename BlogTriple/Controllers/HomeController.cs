using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogTriple.Models;

namespace BlogTriple.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List","Article");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            var db = new BlogDbContext();

            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();

                return RedirectToAction("Details", "Home");
            }

            return View(contact);
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult Hotels()
        {
            return View();
        }

        public ActionResult PlaneTickets()
        {
            return View();
        }

        public ActionResult RentACar()
        {
            return View();
        }
    }
}