using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class BookingItem
    {
        public BookingItem()
        {

        }
        public BookingItem(int BookingId, string Comment, int MantID) : base()
        {
            this.BookingId = BookingId;
            this.Comment = Comment;
            this.MantID = MantID;
        }

        [Key]
        public int BookingId { get; set; }
        public string Comment { get; set; }
        public int MantID { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is BookingItem bookingItem &&
                this.BookingId == bookingItem.BookingId && this.MantID == bookingItem.MantID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, BookingId);
        }
    }
}