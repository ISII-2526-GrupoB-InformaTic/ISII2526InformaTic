using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public IList<Purchase>? Purchases { get; set; }
    public IList<Rental>? Rentals { get; set; }
    public IList<Booking>? Bookings { get; set; }
    public string DeliveryAddress { get; set; }
    public ApplicationUser() { }


    public ApplicationUser(string id, string name, string surname, string email, string deliveryAddress)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        DeliveryAddress = deliveryAddress;
    }
}