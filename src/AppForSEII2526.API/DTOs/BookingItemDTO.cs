using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.API.DTOs
{
    public class BookingItemDTO
    {
        public BookingItemDTO()
        {

        }
        public BookingItemDTO(int BookingId, string Comment, int MantID, BookingDTO Booking, MaintenanceDTO Maintenance) : base()
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
        public BookingDTO Booking { get; set; }
        public MaintenanceDTO Maintenance { get; set; }

    }
}