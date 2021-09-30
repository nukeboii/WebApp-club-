using ClubApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ClubDbContext dbContext;
        public UserController(ClubDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public IActionResult RegisterOrLogin()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("userID");
            return Redirect("/Home/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Member member)
        {
            if(member != null && ModelState.IsValid)
            {
                dbContext.Members.Add(member);
                await dbContext.SaveChangesAsync();
                HttpContext.Session.SetString("user", member.UserName);
                HttpContext.Session.SetInt32("userID", member.UserId);
                return Redirect("/Home/Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Member member)
        {
            if(member != null)
            {
                var _member = dbContext.Members.Where(x=>x.UserName == member.UserName && x.Password == member.Password)?.FirstOrDefault();

                if(_member != null)
                {
                    HttpContext.Session.SetString("user", _member.UserName);
                    HttpContext.Session.SetInt32("userID", _member.UserId);
                    return Redirect("/Home/Index");
                }
            }
            ViewBag.Message = "Invalid Credetials";
            return RedirectToAction(nameof(RegisterOrLogin));
        }
    }
}
