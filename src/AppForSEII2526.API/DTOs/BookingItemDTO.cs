using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class BookingItemDTO
    {
        public BookingItemDTO()
        {

        }

        public BookingItemDTO(string comment, int bookingID, int maintenanceID, string maintName, int price)
        {
            MaintenanceId = maintenanceID;
            BookingId = bookingID;
            Comment = comment;
            MaintName = maintName;
            Price = price;
        }

        public int BookingId { get; set; }
        [StringLength(200, ErrorMessage = "El comentario debe tener menos de 200 caracteres y mas de 20.", MinimumLength = 20)]
        public string Comment { get; set; }
        public int MaintenanceId { get; set; }
        public string MaintName { get; set; }
        public int Price { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingItemDTO dTO &&
                   BookingId == dTO.BookingId &&
                   Comment == dTO.Comment &&
                   MaintenanceId == dTO.MaintenanceId &&
                     MaintName == dTO.MaintName &&
                     Price == dTO.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, Comment, MaintenanceId, MaintName, Price);
        }
    }
}