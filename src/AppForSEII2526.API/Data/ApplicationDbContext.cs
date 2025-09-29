using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<RentalItem>().HasKey(pi => new { pi.CarId, pi.RentalId });
        builder.Entity<Rental>().HasKey(pi => new { pi.Id });
        builder.Entity<Car>().HasKey(pi => new { pi.Id });
    }

    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalItem> RentalItems { get; set; }
    public DbSet<Car> Cars { get; set; }
    
}
