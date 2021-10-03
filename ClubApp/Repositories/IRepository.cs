using ClubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Repositories
{
    public interface IRepository
    {
        public Task<bool> AddEvent(UpcomingEvents upcomingEvents);
        public List<UpcomingEvents> GetUpcomingEvents();
        public Task<bool> AddNews(News news);
        public List<News> GetNews();
    }
}
