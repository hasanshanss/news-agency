using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewsAgency.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NewsAgency.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

      
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategory { get; set; }

    }
}
