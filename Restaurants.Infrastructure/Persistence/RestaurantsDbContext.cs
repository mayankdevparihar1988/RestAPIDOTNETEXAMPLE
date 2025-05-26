using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContext : DbContext
    {
        public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
            : base(options)
        {
        }
        public DbSet<Restaurant> Restaurants { get; set; } = null!;

        public DbSet<Dish> Dishes { get; set; } = null!;

        // <summary>
        //   Not Needed when configuring the DbContext in the Startup.cs file
        // </summary>
        // <param name="modelBuilder"></param>
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost;Database=restaurantDb;Trusted_Connection=True;TrustServerCertificate=True;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address, a =>
            {
                a.WithOwner();
                a.Property(p => p.Street).HasColumnName("Street");
                a.Property(p => p.City).HasColumnName("City");
                a.Property(p => p.State).HasColumnName("State");
                a.Property(p => p.ZipCode).HasColumnName("ZipCode");
            });

            modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes)
                   .WithOne()
                   .HasForeignKey(d => d.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);

           // modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantsDbContext).Assembly);
        }
    }

}
