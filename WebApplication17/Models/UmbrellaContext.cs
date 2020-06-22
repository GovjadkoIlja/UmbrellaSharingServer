using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class UmbrellaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<RateItem> Rates { get; set; }
        public DbSet<RatePlan> RatePlans { get; set; }
        //public DbSet<>
        public DbSet<Point> Points { get; set; }

        public const int RATE_PLAN_ID = 1;

        public UmbrellaContext(DbContextOptions<UmbrellaContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();

            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
