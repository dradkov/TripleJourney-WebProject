using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTriple.Models
{
    public class HotelsViewModel
    {
        [Required]
        [Display(Name ="Destination")]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "From")]
        public string From { get; set; }

        [Required]
        [Display(Name = "To")]
        public string To { get; set; }

        [Required]
        [Display(Name = "Rooms")]
        public string Rooms { get; set; }


    }
}