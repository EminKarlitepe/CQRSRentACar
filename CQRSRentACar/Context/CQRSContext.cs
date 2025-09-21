using CQRSRentACar.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

namespace CQRSRentACar.Context
{
    public class CQRSContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            // bağlantı adresiniz
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarRental>()
                .HasOne(cr => cr.PickUpAirport)
                .WithMany()
                .HasForeignKey(cr => cr.PickUpAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarRental>()
                .HasOne(cr => cr.DropOffAirport)
                .WithMany()
                .HasForeignKey(cr => cr.DropOffAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
