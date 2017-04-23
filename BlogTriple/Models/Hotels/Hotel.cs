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

        public virtual ApplicationUser IdHotel { get; set; }


        public virtual ApplicationUser NameHotel { get; set; }


        public virtual ApplicationUser StarsHotel { get; set; }


        public virtual ApplicationUser PoolHotel { get; set; }


        public virtual ApplicationUser SpaHotel { get; set; }


        public virtual ApplicationUser PricePerNightHotel { get; set; }


        public virtual ApplicationUser FitnessHotel { get; set; }

        public virtual ApplicationUser ImageUrlHotel { get; set; }

        public virtual ApplicationUser TouristHotel { get; set; }



    }
}