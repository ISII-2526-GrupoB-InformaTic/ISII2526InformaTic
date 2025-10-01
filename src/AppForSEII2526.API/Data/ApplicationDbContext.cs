using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<PurchaseItem>().HasKey(pi => new { pi.CarId, pi.PurchaseId });
        
        builder.Entity<Purchase>().HasKey(pi => new { pi.Id });
    
        builder.Entity<Car>().HasKey(pi => new { pi.Id });
   
    }

    public DbSet<Purchase> Purchases { get; set; }

    public DbSet<PurchaseItem> PurchaseItems { get; set; }

    public DbSet<Car> Cars { get; set; }

}
