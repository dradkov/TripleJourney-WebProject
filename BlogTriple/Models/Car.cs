using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogTriple.Models
{
    public class Car
    {
        public int Id { get; set; }
        
        public string Location { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        public DateTime PickUpTime { get; set; }

        public DateTime DropOffTime { get; set; }
    }
}