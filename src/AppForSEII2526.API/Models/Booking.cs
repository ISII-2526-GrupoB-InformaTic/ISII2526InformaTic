using AppForSEII2526.API.Models;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII2526.API.Models
{
    public class Booking
    {
        public Booking()
        {

        }
        public Booking(DateTime Date, PaymentMethod PaymentMethod, ApplicationUser usuario) : base()
        {
            this.Date = Date;
            this.PaymentMethod = PaymentMethod;
            this.User = usuario;
            clientAdress = usuario.DeliveryAddress;
            clientName = usuario.Name;
            clientSurname = usuario.Surname;
            clientPhoneNumber = usuario.PhoneNumber;
        }
        public Booking(DateTime Date, PaymentMethod PaymentMethod, IList<BookingItem> BookingItems, ApplicationUser usuario) : base()
        {
            this.Date = Date;
            this.PaymentMethod = PaymentMethod;
            this.BookingItems = BookingItems;
            this.User = usuario;
            this.UserId = usuario.Id;
            clientAdress = usuario.DeliveryAddress;
            clientName = usuario.Name;
            clientSurname = usuario.Surname;
            clientPhoneNumber = usuario.PhoneNumber;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "La calle debe tener menos de 30 caracteres y mas de 4.", MinimumLength=4)]
        public string clientAdress { get; set; }
        [StringLength(10, ErrorMessage = "El nombre debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string clientName { get; set; }
        [StringLength(12, ErrorMessage = "El telefono movil debe tener entre 9 y 12 digitos", MinimumLength = 9)]
        public string? clientPhoneNumber { get; set; }
        [StringLength(10, ErrorMessage = "El apellido debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string clientSurname { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IList<BookingItem> BookingItems { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int numberOfDays { get; set; }
        public int Price { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is Booking booking &&
                   Id == booking.Id &&
                   UserId == booking.UserId &&
                   clientAdress == booking.clientAdress &&
                   clientName == booking.clientName &&
                   clientPhoneNumber == booking.clientPhoneNumber &&
                   clientSurname == booking.clientSurname &&
                   Date == booking.Date &&
                   numberOfDays == booking.numberOfDays &&
                   Price == booking.Price &&
                   PaymentMethod == booking.PaymentMethod &&
                   BookingItems.SequenceEqual( booking.BookingItems) &&
                   EqualityComparer<ApplicationUser>.Default.Equals(User, booking.User);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(UserId);
            hash.Add(clientAdress);
            hash.Add(clientName);
            hash.Add(clientPhoneNumber);
            hash.Add(clientSurname);
            hash.Add(Date);
            hash.Add(numberOfDays);
            hash.Add(Price);
            hash.Add(PaymentMethod);
            hash.Add(BookingItems);
            hash.Add(User);
            return hash.ToHashCode();
        }
    }
}