using GenericRepository.EntityFramework.SampleCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.EntityFramework.SampleCore
{
    public class AccommodationEntities : EntitiesContext
    {
        //public AccommodationEntities(DbContextOptions<EntitiesContext> context) : base(context) { }
        public AccommodationEntities(DbContextOptions context) : base(context) { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Resort> Resorts { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Resort>().ToTable("Resort");
            modelBuilder.Entity<Hotel>().ToTable("Hotel");
        }
    }
}