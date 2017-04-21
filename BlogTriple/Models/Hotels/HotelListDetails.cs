using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTriple.Models.Hotels
{
    public class HotelListDetails
    {

        public int Id { get; set; }

        public string Name { get; set; }


        public string Stars { get; set; }


        public string Pool { get; set; }


        public string Spa { get; set; }


        public decimal PricePerNight { get; set; }


        public string Fitness { get; set; }

        public string ImageUrl { get; set; }
    }
}