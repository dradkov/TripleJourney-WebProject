using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogTriple.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Pick-up Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PickUpDate { get; set; }

        [Required]
        [Display(Name = "Drop-off Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DropOffDate { get; set; }

        [Required]
        [Display(Name = "Pick-up Time")]
        [DataType(DataType.Time)]
        public DateTime PickUpTime { get; set; }

        [Required]
        [Display(Name = "Drop-off Time")]
        [DataType(DataType.Time)]
        public DateTime DropOffTime { get; set; }
    }
}