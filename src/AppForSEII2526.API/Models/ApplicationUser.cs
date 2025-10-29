using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    
    public int Id {  get; set; }
    public string Name { get; set; }
    public string Surname   { get; set; }
    public string Email {  get; set; }
    public IList<Purchase> Purchases { get; set; }
    public IList<Rental> Rentals { get; set; }
    public IList<Booking> Bookings { get; set; }

    public ApplicationUser() { }

    public ApplicationUser(int id, string name, string surname, string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }
}