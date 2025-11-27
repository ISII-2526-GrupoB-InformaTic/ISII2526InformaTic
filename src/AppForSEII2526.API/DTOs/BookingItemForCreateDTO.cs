using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class BookingItemForCreateDTO
    {
        public BookingItemForCreateDTO()
        {

        }

        public string Comment { get; set; }
        public int MaintenanceId { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is BookingItemForCreateDTO dTO &&
                   Comment == dTO.Comment &&
                   MaintenanceId == dTO.MaintenanceId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Comment, MaintenanceId);
        }
    }
}