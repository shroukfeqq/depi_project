using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace MVC_Project.Models
{
    public class Ticket
    {
        [Key]
        public string Ticket_Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public int MatchId { get; set; }
        public Match Match { get; set; }

        public DateTime BookingDate { get; set; }
      
    }
}
