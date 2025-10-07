using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<RentalItem>().HasKey(pi => new { pi.CarId, pi.RentalId });
        builder.Entity<Rental>().HasKey(pi => new { pi.Id });
        builder.Entity<Car>().HasKey(pi => new { pi.Id });
        builder.Entity<Booking>().HasKey(pi => new {pi.Id });
        builder.Entity<Model>().HasKey(pi => new { pi.Id });
        builder.Entity<Purchase>().HasKey(pi => new { pi.Id });
        builder.Entity<PurchaseItem>().HasKey(pi => new { pi.PurchaseId,pi.CarId });
        builder.Entity<BookingItem>().HasKey(pi => new { pi.BookingId, pi.MantID });
        builder.Entity<Maintenance>().HasKey(pi => new { pi.Id });
        builder.Entity<MaintenanceType>().HasKey(pi => new { pi.Id });
    }

    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalItem> RentalItems { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<PurchaseItem> PurchaseItems { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<MaintenanceType> MaintenanceTypes { get; set; }
    public DbSet<BookingItem> BookingItems { get; set; }
}