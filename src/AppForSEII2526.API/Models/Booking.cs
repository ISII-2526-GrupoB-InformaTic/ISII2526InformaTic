using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class Booking
    {
        public Booking()
        {

        }
        public Booking(string clientAddress, string clientName, int clientPhoneNumber, string clientSurname, string Date, int Id, string PaymentMethod, IList<BookingItem> BookingItems) : base()
        {
            this.clientAdress = clientAddress;
            this.clientName = clientName;
            this.clientPhoneNumber = clientPhoneNumber;
            this.clientSurname = clientSurname;
            this.Date = Date;
            this.Id = Id;
            this.PaymentMethod = PaymentMethod;
            this.BookingItems = BookingItems;
        }

        [Key]
        public int Id { get; set; }
        public string clientAdress { get; set; }
        public string clientName { get; set; }
        public int clientPhoneNumber { get; set; }
        public string clientSurname { get; set; }
        public string Date { get; set; }
        public string PaymentMethod { get; set; }
        public IList<BookingItem> BookingItems { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Booking booking &&
                   Id == booking.Id &&
                   clientAdress == booking.clientAdress &&
                   clientName == booking.clientName &&
                   clientPhoneNumber == booking.clientPhoneNumber &&
                   clientSurname == booking.clientSurname &&
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