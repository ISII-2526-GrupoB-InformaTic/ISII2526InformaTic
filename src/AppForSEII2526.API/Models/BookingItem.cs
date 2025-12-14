using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class BookingItem
    {
        public BookingItem()
        {

        }
        public BookingItem(string comment, Booking booking, Maintenance maintenance)
        {
            Comment = comment;
            Booking = booking;
            Maintenance = maintenance;

            BookingId = booking.Id;
            MaintenanceId = maintenance.Id;
            Price=maintenance.Price;
            MaintName=maintenance.Name;
            NumberOfDays=maintenance.NumberOfDays;
        }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; }
        public string MaintName { get; set; }
        public int Price { get; set; }
        public int NumberOfDays { get; set; }

        [StringLength(200, ErrorMessage = "El comentario debe tener menos de 200 caracteres y mas de 20.", MinimumLength = 20)]
        public string Comment { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BookingItem item &&
                   BookingId == item.BookingId &&
                   Comment == item.Comment &&
                   MaintenanceId == item.MaintenanceId &&
                   Price == item.Price &&
                   MaintName == item.MaintName &&
                     NumberOfDays == item.NumberOfDays &&
                   EqualityComparer<Booking>.Default.Equals(Booking, item.Booking) &&
                   EqualityComparer<Maintenance>.Default.Equals(Maintenance, item.Maintenance);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookingId, Comment, MaintenanceId,Price,MaintName,NumberOfDays, Booking, Maintenance);
        }
    }
}