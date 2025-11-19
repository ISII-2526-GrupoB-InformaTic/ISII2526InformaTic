using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class BookingItemDTO
    {
        public BookingItemDTO()
        {

        }

        public BookingItemDTO(string comment, BookingDTO booking, MaintenanceDTO maintenance)
        {
            Booking = booking;
            Maintenance = maintenance;

            BookingId = booking.Id;
            MaintenanceId = maintenance.Id;
            Comment = comment;
        }

        public int BookingId { get; set; }
        [StringLength(200, ErrorMessage = "El comentario debe tener menos de 200 caracteres y mas de 20.", MinimumLength = 20)]
        public string Comment { get; set; }
        public int MaintenanceId { get; set; }

        public BookingDTO Booking { get; set; }
        public MaintenanceDTO Maintenance { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingItemDTO dTO &&
                   BookingId == dTO.BookingId &&
                   Comment == dTO.Comment &&
                   MaintenanceId == dTO.MaintenanceId &&
                   EqualityComparer<MaintenanceDTO>.Default.Equals(Maintenance, dTO.Maintenance);
                   EqualityComparer<BookingDTO>.Default.Equals(Booking, dTO.Booking);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, Comment, MaintenanceId, Booking, Maintenance);
        }
    }
}