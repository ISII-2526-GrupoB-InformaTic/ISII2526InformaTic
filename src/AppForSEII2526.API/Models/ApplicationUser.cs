using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser {
    private string Username;
    private string nombre;
    private string apellido;
    private string email;

    public ApplicationUser(string userName, string Nombre, string Apellido, string Email) : base(userName)
    {
        this.UserName = userName;
        this.nombre = Nombre;
        this.apellido = Apellido;
        this.email = Email;
    }

    public IList<Purchase> Purchases { get; set; }
    public IList<Rental> Rentals { get; set; }
    public IList<Booking> Bookings { get; set; }

}