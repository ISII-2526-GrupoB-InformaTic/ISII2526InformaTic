using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    

    public IList<Purchase> Purchases { get; set; }
    public IList<Rental> Rentals { get; set; }

    public ApplicationUser(IList<Purchase> purchases, IList<Rental> rentals)
    {
        Purchases = purchases;
        Rentals = rentals;
    }
}