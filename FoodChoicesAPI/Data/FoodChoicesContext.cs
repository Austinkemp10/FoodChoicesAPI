using FoodChoicesAPI.Models;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodChoicesAPI.Data
{
    public class FoodChoicesContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public FoodChoicesContext(DbContextOptions<FoodChoicesContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=foodchoices;user=root;password=root");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Age);
                entity.Property(e => e.DateCreated);
                entity.Property(e => e.Deleted);
            });

            modelBuilder.Entity<Restaurants>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Category).IsRequired();
                entity.Property(e => e.City);
                entity.Property(e => e.State);
                entity.Property(e => e.County);
                entity.Property(e => e.ImageName);
            });

            modelBuilder.Entity<Coordinate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RestaurantId).IsRequired();
                entity.Property(e => e.Latitude).IsRequired();
                entity.Property(e => e.Longitude).IsRequired();
            });

        }
    }
}
