using ClubApp.Models;
using ClubApp.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly ClubDbContext dbContext;
        private readonly IRepository _repo;
        private readonly IWebHostEnvironment webHostEnvironment;
        public NewsController(IRepository repo, IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
            _repo = repo;
        }
        public IActionResult Index()
        {
            var news = _repo.GetNews();
            if (news.Count > 0)
            {
                Random random = new Random();
                int n = news.Count;

                for (int i = news.Count - 1; i > 1; i--)
                {
                    int rnd = random.Next(i + 1);

                    News value = news[rnd];
                    news[rnd] = news[i];
                    news[i] = value;
                }
            }
            return View(news.Take(3).ToList());
        }
        public IActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNews(News news)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files.FirstOrDefault();
                string uniqueFileName = UploadedFile(file);
                news.ImageName = uniqueFileName;
                news.UserId = HttpContext.Session.GetInt32("userID").Value;

                await _repo.AddNews(news);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file.FileName != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "resources");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
