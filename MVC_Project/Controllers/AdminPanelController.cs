using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.ViewModels;
using System.Security.Claims;

namespace MVC_Project.Controllers
{
     [Authorize(Roles = "admin")]
    public class AdminPanelController : Controller
    {
        AppDbContext context = new AppDbContext();

        
    
        public IActionResult AdminPanel()
        {
            var matches = context.Matches.ToList();
            return View(matches);
        }

      
        public IActionResult CreateMatch()
        {
            return View();
        }

        [HttpPost]
      
        public IActionResult SaveMatch(MatchViewModel req)
        {
            if (ModelState.IsValid)
            {
                Match mm = new Match
                {

                    Name = req.Name,
                    TeamA = req.TeamA,
                    TeamB = req.TeamB,
                    Location = req.Location,
                    Date = req.Date,
                    AvailableTickets = req.AvailableTickets,

                };
                context.Matches.Add(mm);
                context.SaveChanges();
                ViewData["msg"] = "A new match is added succssfully";
                return View("Send Success");
            }
            return View("CreateMatch", req);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Match match = context.Matches.FirstOrDefault(p => p.Match_Id == id);
            return View(match);
        }
        public IActionResult saveEdit(int id, MatchViewModel newMatch)
        {
            Match noldMatch = context.Matches.FirstOrDefault(p => p.Match_Id == id);

            if (ModelState.IsValid)
            {
                noldMatch.Name = newMatch.Name;
                noldMatch.TeamA = newMatch.TeamA;
                noldMatch.TeamB = newMatch.TeamB;
                noldMatch.Date = newMatch.Date;
                noldMatch.Location = newMatch.Location;
                noldMatch.AvailableTickets = newMatch.AvailableTickets;
                context.SaveChanges();
                ViewData["msg"] = "The match is Updated succssfully";
                return View("Send Success");
            }
            else return View("Edit", newMatch);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var match = context.Matches.Find(id);
            if (match == null)
            {
                ViewData["msg"] = "There  is No Data to delete ";
                return View("Send Success");
            }
            return View(match);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var match = context.Matches.Find(id);

            if (match != null)
            {
                context.Matches.Remove(match);
                context.SaveChanges();
                ViewData["msg"] = "Match deleted successfully";

                return View("Send Success");
            }
            else
            {
                ViewData["msg"] = "Match not found";
            }
            return View("Send Success");

            return RedirectToAction("AdminPanel");
        }




    }

}






