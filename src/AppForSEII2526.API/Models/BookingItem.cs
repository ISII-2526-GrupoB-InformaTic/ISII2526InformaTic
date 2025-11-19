using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class BookingItem
    {
        public BookingItem()
        {

        }
        public BookingItem(string Comment, Booking Booking, Maintenance Maintenance) : base()
        {
            this.Comment = Comment;
            this.Booking = Booking;
            this.Maintenance = Maintenance;
            this.BookingId = Booking.Id;
            this.MantID = Maintenance.Id;
        }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; }

        [StringLength(200, ErrorMessage = "El comentario debe tener menos de 200 caracteres y mas de 20.", MinimumLength = 20)]
        public string Comment { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingItem item &&
                   BookingId == item.BookingId &&
                   Comment == item.Comment &&
                   MaintenanceId == item.MaintenanceId &&
                   EqualityComparer<Booking>.Default.Equals(Booking, item.Booking) &&
                   EqualityComparer<Maintenance>.Default.Equals(Maintenance, item.Maintenance);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, Comment, MaintenanceId, Booking, Maintenance);
        }
    }
}