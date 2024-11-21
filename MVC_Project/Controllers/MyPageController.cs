using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using System.Security.Claims;

namespace MVC_Project.Controllers
{
    [Authorize(Roles ="user")]
    public class MyPageController : Controller
   

    {
        AppDbContext context = new AppDbContext();

        public IActionResult UserTickets()
        {
          var userId =GetUserId(); 

            var tickets = context.Tickets
                                 .Include(t => t.Match) 

                                 .Where(t => t.UserId == userId)
                                 .ToList();

           

            return View(tickets); 
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
