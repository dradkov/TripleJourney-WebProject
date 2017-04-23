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
        public ActionResult AllHotels(BookedOrder orders)
        {

            if (ModelState.IsValid)
            {
                var database = new BlogDbContext();

                var touristId = this.User.Identity.GetUserId();

                var hotelInfo = database.Hotels.Select(h => new CreateOrder
                {
                    Name = h.NameHotel.Name,
                    Stars = h.StarsHotel.Stars,
                    Pool = h.PoolHotel.Pool,
                    Spa = h.SpaHotel.Spa,
                    Fitness = h.FitnessHotel.Fitness,
                    ImageUrl = h.ImageUrlHotel.ImageUrl,
                    PricePerNight = h.PricePerNightHotel.PricePerNight,
                    TouristId = h.TouristHotel.TouristId
                }).FirstOrDefault();

                var destinationInfo = database.Destinations.Select(d => new Destination
                {
                    Id = d.Id,
                    Town = d.TownDestination.Town,
                    From = d.FromDestination.From,
                    To = d.ToDestination.To,
                    Price = d.PriceDestination.Price
                }).FirstOrDefault();

                var startDate = new DateTime(orders.From.Year, orders.From.Month, orders.From.Day);
                var endDate = new DateTime(orders.To.Year, orders.To.Month, orders.To.Day);

                var numDays = endDate.Subtract(startDate);
                var convertDays = numDays.TotalDays;

                decimal result = 0;

                if (orders.Rooms == "1")
                {
                    result = (decimal)convertDays * (orders.Price * orders.PricePerNight);
                }
                else if (orders.Rooms == "2")
                {
                    result =
                        (decimal)convertDays * (orders.Price * orders.PricePerNight) * 2;
                }
                else if (orders.Rooms == "3")
                {
                    result =
                        (decimal)convertDays * (orders.Price * orders.PricePerNight) * 3;
                }
                else if (orders.Rooms == "4")
                {
                    result =
                        ((decimal)convertDays * (orders.Price * orders.PricePerNight)) * 4;
                };

                orders.Price = result;
                database.BookedOrder.Add(orders);
                database.SaveChanges();

                return RedirectToAction("BookingDetails", new { id = orders.Id });
            }
            return View();
        }

        public ActionResult BookingDetails()
        {
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

                //var startDate = new DateTime(city.From.Year, city.From.Month, city.From.Day);
                //var endDate = new DateTime(city.To.Year, city.To.Month, city.To.Day);

                //var numDays = endDate.Subtract(startDate);
                //var convertDays = numDays.TotalDays;

                //decimal result = 0;


                //if (city.Rooms == "1")
                //{
                //    result = (decimal)convertDays * 20;
                //}
                //else if (city.Rooms == "2")
                //{
                //    result = (decimal)convertDays * 30;
                //}
                //else if (city.Rooms == "3")
                //{
                //    result = (decimal)convertDays * 40;
                //}
                //else if (city.Rooms == "4")
                //{
                //    result = (decimal)convertDays * 60;
                //}

                //city.Price = result;
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