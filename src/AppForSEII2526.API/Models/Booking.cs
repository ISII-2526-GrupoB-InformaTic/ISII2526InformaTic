using AppForSEII2526.API.Models;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace AppForSEII2526.API.Models
{
    public class Booking
    {
        public Booking()
        {

        }
        public Booking(string clientAddress,string clientSurname, string Date, int Id, string PaymentMethod, IList<BookingItem> BookingItems, ApplicationUser usuario ) : base()
        {
            this.clientAdress = clientAddress;
            this.Date = Date;
            this.Id = Id;
            this.PaymentMethod = PaymentMethod;
            this.BookingItems = BookingItems;
            this.clientSurname = clientSurname;
            this.User = usuario;
            clientName = usuario.UserName;
            clientPhoneNumber = usuario.PhoneNumber;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "La calle debe tener menos de 30 caracteres y mas de 4.", MinimumLength=4)]
        public string clientAdress { get; set; }
        [StringLength(10, ErrorMessage = "El nombre debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string clientName { get; set; }
        [StringLength(12, ErrorMessage = "El telefono movil debe tener entre 9 y 12 digitos", MinimumLength = 9)]
        public string clientPhoneNumber { get; set; }
        [StringLength(10, ErrorMessage = "El apellido debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string clientSurname { get; set; }
        [DataType(DataType.Date)]
        public string Date { get; set; }
        public string PaymentMethod { get; set; }
        public IList<BookingItem> BookingItems { get; set; }
        public ApplicationUser User { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Booking booking &&
                   Id == booking.Id &&
                   clientAdress == booking.clientAdress &&
                   clientPhoneNumber == booking.clientPhoneNumber &&
                   Date == booking.Date &&
                   PaymentMethod == booking.PaymentMethod &&
                   EqualityComparer<IList<BookingItem>>.Default.Equals(BookingItems, booking.BookingItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, clientAdress, clientName, clientPhoneNumber, clientSurname, Date, PaymentMethod, BookingItems);
        }
    }
}