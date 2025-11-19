using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.API.DTOs
{
    public class BookingItemDTO
    {
        public BookingItemDTO()
        {

        }
        public BookingItemDTO(string Comment, BookingDTO Booking, MaintenanceDTO Maintenance) : base()
        {
            this.Booking = Booking;
            this.Maintenance = Maintenance;
            BookingId = Booking.Id;
            this.Comment = Comment;
            MantID = Maintenance.Id;
        }

        public int BookingId { get; set; }
        [StringLength(200, ErrorMessage = "El comentario debe tener menos de 200 caracteres y mas de 20.", MinimumLength = 20)]
        public string Comment { get; set; }
        public int MantID { get; set; }
        public BookingDTO Booking { get; set; }
        public MaintenanceDTO Maintenance { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingItemDTO dTO &&
                   BookingId == dTO.BookingId &&
                   Comment == dTO.Comment &&
                   MantID == dTO.MantID &&
                   EqualityComparer<MaintenanceDTO>.Default.Equals(Maintenance, dTO.Maintenance);
                   EqualityComparer<BookingDTO>.Default.Equals(Booking, dTO.Booking);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, Comment, MantID, Booking, Maintenance);
        }
    }
}