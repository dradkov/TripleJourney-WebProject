using BlogTriple.Models;
using BlogTriple.Models.Hotels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogTriple.Controllers
{
    public class HotelsController : Controller
    {

        public ActionResult AllHotels()
        {
            var database = new BlogDbContext();

            var hotels = database.Hotels.Select(h => new HotelListDetails
            {
                Id = h.Id,                  
                Name = h.Name,
                Fitness = h.Fitness,
                Stars = h.Stars,
                Spa = h.Spa,
                Pool = h.Pool,
                ImageUrl = h.ImageUrl,
                PricePerNight = h.PricePerNight

            }).ToList();

            return View(hotels);
        }

        [HttpPost]
        public ActionResult AllHotels(CreateOrder orders)
        {
            if (ModelState.IsValid)
            {
                var database = new BlogDbContext();

               var order = database.FinalHotelOrders.Select(o => new HotelOrdrerDetails
                {
                    Id = o.Id,
                    Name = o.Name,
                    Fitness = o.Fitness,
                    Stars = o.Stars,
                    Spa = o.Spa,
                    Pool = o.Pool,
                    ImageUrl = o.ImageUrl,
                    PricePerNight = o.PricePerNight,
                   Town = o.TownA.Town,
                   From = o.FromA.From,
                   To = o.ToA.To,
                   Rooms = o.RoomsA.Rooms,
                   Price = o.PriceA.Price
               }).FirstOrDefault(); 

                //database.FinalHotelOrders.Add(orders);
                database.SaveChanges();

                return RedirectToAction("HotelDetails", new { id = order.Id });
            }
            return View();
           
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateHotels hotelModel)
        {
            if (ModelState.IsValid)
            {
                var database = new BlogDbContext();

                var touristId = this.User.Identity.GetUserId();

                var hotel = new Hotel
                {
                    Name = hotelModel.Name,                  
                    Stars = hotelModel.Stars,
                    Pool = hotelModel.Pool,                   
                    Spa = hotelModel.Spa,
                    Fitness = hotelModel.Fitness,
                    ImageUrl = hotelModel.ImageUrl,
                    PricePerNight = hotelModel.PricePerNight,
                    TouristId = touristId

                };

                database.Hotels.Add(hotel);
                database.SaveChanges();

                return RedirectToAction("HotelDetails", new { id = hotel.Id });
            }

            return View(hotelModel);
        }

        
       

        public ActionResult HotelDetails(int id)
        {
            var database = new BlogDbContext();
            var hotel = database.Hotels.Where(h => h.Id == id)
                .Select(h => new HotelDetailsModel
                {
                    Name = h.Name,
                    Stars = h.Stars,
                    Fitness = h.Fitness,
                    Pool = h.Pool,
                    Spa = h.Spa,
                    ImageUrl = h.ImageUrl,
                    PricePerNight = h.PricePerNight
                }).FirstOrDefault();

            if (hotel == null)
            {
                return HttpNotFound();
            }

            return View(hotel);

        }




        [HttpGet]
        [Authorize]
        public ActionResult Destination()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Destination(Destination city)
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

                return RedirectToAction("AllHotels", new { id = city.Id });
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