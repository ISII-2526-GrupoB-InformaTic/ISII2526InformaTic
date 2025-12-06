using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class BookingItemDTO
    {
        public BookingItemDTO()
        {

        }

        public BookingItemDTO(string comment, int bookingID, int maintenanceID)
        {
            MaintenanceId = maintenanceID;
            BookingId = bookingID;
            Comment = comment;
        }

        public int BookingId { get; set; }
        [StringLength(200, ErrorMessage = "El comentario debe tener menos de 200 caracteres y mas de 20.", MinimumLength = 20)]
        public string Comment { get; set; }
        public int MaintenanceId { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingItemDTO dTO &&
                   BookingId == dTO.BookingId &&
                   Comment == dTO.Comment &&
                   MaintenanceId == dTO.MaintenanceId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, Comment, MaintenanceId);
        }
    }
}