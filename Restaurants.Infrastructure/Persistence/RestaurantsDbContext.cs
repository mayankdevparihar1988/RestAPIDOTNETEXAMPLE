using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    public class RestaurantsDbContext : DbContext
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
            // Configure the Restaurant entity to own an Address value object.
            // This means Address properties will be stored in the Restaurants table as columns,
            // not as a separate table. The configuration below maps each Address property to a column
            // with a specific name in the Restaurants table.
            modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address, a =>
            {
                // Specifies that the Address is owned by the Restaurant entity.
                a.WithOwner();

                // Map the Street property of Address to a column named "Street".
                a.Property(p => p.Street).HasColumnName("Street");

                // Map the City property of Address to a column named "City".
                a.Property(p => p.City).HasColumnName("City");

                // Map the State property of Address to a column named "State".
                a.Property(p => p.State).HasColumnName("State");

                // Map the ZipCode property of Address to a column named "ZipCode".
                a.Property(p => p.ZipCode).HasColumnName("ZipCode");
            });

            // This line configures the relationship between the Restaurant and Dish entities using the Entity Framework Core Fluent API.
            // It specifies that a Restaurant can have many Dishes (one-to-many relationship).
            // The .WithOne() call means each Dish is associated with one Restaurant, but no navigation property is defined on the Dish side.
            // The .HasForeignKey(d => d.RestaurantId) sets up the foreign key in the Dishes table, linking each Dish to its Restaurant via the RestaurantId property.
            // The .OnDelete(DeleteBehavior.Cascade) ensures that if a Restaurant is deleted, all related Dishes are also deleted from the database automatically.
            modelBuilder.Entity<Restaurant>().HasMany(r => r.Dishes)
                   .WithOne()
                   .HasForeignKey(d => d.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);

           // modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantsDbContext).Assembly);
        }
    }

}
