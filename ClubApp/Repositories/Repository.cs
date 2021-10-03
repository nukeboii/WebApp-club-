using ClubApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Repositories
{
    public class Repository : IRepository
    {
        private readonly ClubDbContext dbContext;
        public Repository(ClubDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<bool> AddEvent(UpcomingEvents upcomingEvents)
        {
            bool result = false;
            try
            {
                dbContext.UpcomingEvents.Add(upcomingEvents);
                await dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<bool> AddNews(News news)
        {
            bool result = false;
            try
            {
                dbContext.News.Add(news);
                await dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public List<News> GetNews()
        {
            var news = dbContext.News.Include(x => x.Member).ToList();
            foreach (var _news in news)
            {
                _news.Member = dbContext.Members.Find(_news.UserId);
            }
            return news;
        }

        public List<UpcomingEvents> GetUpcomingEvents()
        {
            var events = dbContext.UpcomingEvents.Include(x => x.Member).Where(x => x.CompetitionDate >= DateTime.Now)?.ToList();
            foreach (var _event in events)
            {
                _event.Member = dbContext.Members.Find(_event.UserId);
            }

            return events;
        }

    }
}
