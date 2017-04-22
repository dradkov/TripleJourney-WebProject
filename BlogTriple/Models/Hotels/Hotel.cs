using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTriple.Models
{
    public class Hotel
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Stars { get; set; }

        [Required]
        public string Pool { get; set; }

        [Required]
        public string Spa { get; set; }

        [Required]
        [Display(Name = "Price Per Night in €")]
        public decimal PricePerNight { get; set; }

        [Required]
        public string Fitness { get; set; }

        public string ImageUrl { get; set; }

        public string TouristId { get; set; }

        public virtual ApplicationUser Tourist { get; set; }

        

    }
}