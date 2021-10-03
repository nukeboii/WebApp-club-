using ClubApp.Models;
using ClubApp.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.IO;
using Xunit;

namespace ClubApp.Test
{
    public class ClubAppTests
    {
        private IRepository _repo;
        IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

       

        public ClubAppTests()
        {
            var connectionString = configuration.GetConnectionString("DBConnection");

            var options = new DbContextOptionsBuilder<ClubDbContext>()
                             .UseSqlServer(new SqlConnection(connectionString))
                             .Options;
            _repo = new Repository(new ClubDbContext(options));
        }

        [Fact]
        public async void CanUserPostNews()
        {
            News news = new News();
            news.CreatedDate = DateTime.Now;
            news.Description = "Sample Description";
            news.ImageName = "img.jpg";
            news.NewsTitle = "New Title";
            bool isValid = await _repo.AddNews(news);

            Assert.True(isValid);
        }
        [Fact]
        public async void CanUserPostevent()
        {
            UpcomingEvents events = new UpcomingEvents();
            events.CompetitionDate = DateTime.Now;
            events.CompetitionLocation = "NY";
            events.CompetitionName = "Competition Name";
            
            bool isValid = await _repo.AddEvent(events);

            Assert.True(isValid);
        }
    }
}
