using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
           : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "The one with that big park."
                },
                new City("Kaspi")
                {
                    Id = 2,
                    Description = "The Saakadze Tower."
                },
                 new City("Barcelona")
                 {
                     Id = 3,
                     Description = "Best architecture in the world."
                 }
                );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The most visited urban park in the United States"
                },
                new PointOfInterest("Rkoni Kheoba")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "Rkoni Kheoba is one of the best place in whole Kaspi"
                });

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfigure(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
