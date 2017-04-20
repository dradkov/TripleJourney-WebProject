using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTriple.Models
{
    public class HotelDetailsModel
    {
        public string Name { get; set; }


        public string Stars { get; set; }


        public string Pool { get; set; }


        public string Spa { get; set; }


        public decimal PricePerNight { get; set; }


        public string Fitness { get; set; }

        public string ImageUrl { get; set; }
    }
}