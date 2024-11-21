using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System.Security.Claims;

namespace MVC_Project.Controllers
{
    [Authorize(Roles ="user")] 
    public class ContentController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public ContentController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Details(int id)
        {
            Match match = context.Matches.FirstOrDefault(p => p.Match_Id == id);
            return View(match);
           
        }
       


        AppDbContext context=new AppDbContext();    
       
       
        public IActionResult MatchList()
        {
            var matches = context.Matches.ToList();
            return View(matches);
        }


        public IActionResult BookingDetails(string ticketId)
        {
            var ticket = context.Tickets.Include(t => t.Match)
                .FirstOrDefault(t => t.Ticket_Id == ticketId);

           

            return View(ticket);
        }



       
        public IActionResult BookTicket(int matchId)
        {
            var match = context.Matches.FirstOrDefault(m => m.Match_Id == matchId);

            if ( match.AvailableTickets == 0)
            {
                return NotFound("Match not found or no tickets available");
            }
       
            var ticket = new Ticket
            {
                UserId = GetUserId(),
                MatchId = match.Match_Id,
                
                    Ticket_Id = Guid.NewGuid().ToString(), 
                                                           
                
            };

            match.AvailableTickets -= 1;
            context.Tickets.Add(ticket);
            context.SaveChanges();

            return RedirectToAction("BookingDetails", new { ticketId = ticket.Ticket_Id });
        }

        private string GetUserId()
        {
           
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                return userIdClaim.Value; 
            }
            throw new Exception("User is not authenticated");
        }
    }
}
