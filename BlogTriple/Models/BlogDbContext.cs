using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BlogTriple.Models.Hotels;

namespace BlogTriple.Models
{


    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Article> Articles { get; set; }

        public virtual IDbSet<Contact> Contacts { get; set; }

        public virtual IDbSet<Destination> Destinations { get; set; }

        public virtual IDbSet<Hotel> Hotels { get; set; }

        public virtual IDbSet<Car> Cars { get; set; }

       public virtual IDbSet<BookedOrder> BookedOrder { get; set; }

        public virtual IDbSet<CreateCar> RentCars { get; set; }


        public object FinalHotelOrder { get; internal set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }
    }
}