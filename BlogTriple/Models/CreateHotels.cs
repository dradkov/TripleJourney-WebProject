using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTriple.Models
{
    public class CreateHotels
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Stars { get; set; }

        [Required]
        public string Pool { get; set; }

        [Required]
        public string Spa { get; set; }

        [Required]
        public string Fitness { get; set; }

        public string ImageUrl { get; set; }
    }
}