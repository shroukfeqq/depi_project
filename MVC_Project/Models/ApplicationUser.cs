using Microsoft.AspNetCore.Identity;

namespace MVC_Project.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string  Phone { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
