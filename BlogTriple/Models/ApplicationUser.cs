using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BlogTriple.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Hotels = new HashSet<Hotel>();
        }

        public virtual ICollection<Hotel> Hotels { get; set; }



        [Required]
        public string FullName { get; set; }

        //public int Id { get; set; }

        [Required]        
        public string Town { get; set; }

        [Required]        
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        [Required]
        public string Rooms { get; set; }


        public decimal Price { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}