using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTriple.Models
{
    public class HotelsViewModel
    {

        public int Id { get; set; }

        [Required]
        [Display(Name ="Destination")]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "From")]
        public DateTime From { get; set; }

        [Required]
        [Display(Name = "To")]
        public DateTime To { get; set; }

        [Required]
        [Display(Name = "Rooms")]
        public string Rooms { get; set; }


    }
}