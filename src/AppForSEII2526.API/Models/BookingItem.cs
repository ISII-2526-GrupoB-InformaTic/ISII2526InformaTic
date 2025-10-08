using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class BookingItem
    {
        public BookingItem()
        {

        }
        public BookingItem(int BookingId, string Comment, int MantID, Booking Booking, Maintenance Maintenance) : base()
        {
            this.BookingId = BookingId;
            this.Comment = Comment;
            this.MantID = MantID;
            this.Booking = Booking;
            this.Maintenance = Maintenance;
        }

        public int BookingId { get; set; }
        [StringLength(200, ErrorMessage = "El comentario debe tener menos de 200 caracteres y mas de 20.", MinimumLength = 20)]
        public string Comment { get; set; }
        public int MantID { get; set; }
        public Booking Booking { get; set; }
        public Maintenance Maintenance { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingItem item &&
                   BookingId == item.BookingId &&
                   Comment == item.Comment &&
                   MantID == item.MantID &&
                   EqualityComparer<Booking>.Default.Equals(Booking, item.Booking) &&
                   EqualityComparer<Maintenance>.Default.Equals(Maintenance, item.Maintenance);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, Comment, MantID, Booking, Maintenance);
        }
    }
}