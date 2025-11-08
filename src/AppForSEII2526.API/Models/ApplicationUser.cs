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

    public ApplicationUser() { }
    IdentityUser user = new IdentityUser();
    
    public ApplicationUser(string id, string name, string surname, string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;

    }
}