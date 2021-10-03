using Microsoft.AspNetCore.Http;
using ClubApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClubApp.Repositories;

namespace ClubApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IRepository _repo;
        public EventsController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var events = _repo.GetUpcomingEvents();
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
                await _repo.AddEvent(upcomingEvents);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
