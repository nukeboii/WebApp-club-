using Microsoft.AspNetCore.Http;
using ClubApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClubApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly ClubDbContext dbContext;
        public EventsController(ClubDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public IActionResult Index()
        {
            var events = dbContext.UpcomingEvents.Include(x => x.Member).Where(x => x.CompetitionDate >= DateTime.Now)?.ToList();
            foreach (var _event in events)
            {
                _event.Member = dbContext.Members.Find(_event.UserId);
            }
            return View(events);
        }
        public IActionResult AddEvent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEvent(UpcomingEvents upcomingEvents)
        {
            if (ModelState.IsValid)
            {
                upcomingEvents.UserId = HttpContext.Session.GetInt32("userID").Value;
                dbContext.UpcomingEvents.Add(upcomingEvents);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
