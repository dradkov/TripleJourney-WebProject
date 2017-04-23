using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTriple.Models.Hotels
{
    public class BookedHotel
    {
        [Key]
        public int Id { get; set; }


        public string Town { get; set; }


        public DateTime From { get; set; }


        public DateTime To { get; set; }


        public string Rooms { get; set; }


        public decimal Price { get; set; }


        public string Name { get; set; }


        public string Stars { get; set; }


        public string Pool { get; set; }


        public string Spa { get; set; }


        public decimal PricePerNight { get; set; }


        public string Fitness { get; set; }

        public string ImageUrl { get; set; }

        public string TouristId { get; set; }
    }
}