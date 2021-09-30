using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Models
{
    public class ClubDbContext : DbContext
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public ClubDbContext(DbContextOptions<ClubDbContext> options) : base(options)
        {

        }
        /// <summary>
        /// Entities declaration
        /// </summary>
        public DbSet<Member> Members { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<UpcomingEvents> UpcomingEvents { get; set; }
      
        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
