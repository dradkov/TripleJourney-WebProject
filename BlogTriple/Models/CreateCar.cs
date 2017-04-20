using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using BlogTriple.Helpers;

namespace BlogTriple.Models
{
    public class CreateCar
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string ModelCar { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }

        public int Power { get; set; }

        public decimal PricePerDay { get; set; }

        [ImageUrl]
        public string ImageUrl { get; set; }
    }
}