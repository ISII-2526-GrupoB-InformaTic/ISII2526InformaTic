namespace AppForSEII2526.API.Models
{
    public class Rental
    {
        public Rental()
        {

        }

        public Rental(int id, DateTime endDate, DateTime startDate, DateTime rentingDate,
            int totalPrice, string deliveryCarDealer, IList<RentalItem> rentalItems)
        {
            Id = id;
            EndDate = endDate;
            StartDate = startDate;
            RentingDate = rentingDate;
            TotalPrice = totalPrice;
            DeliveryCarDealer = deliveryCarDealer;
            RentalItems = rentalItems;
        }
        [Key]
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime RentingDate { get; set; }
        public int TotalPrice { get; set; }
        public string DeliveryCarDealer { get; set; }
        public IList<RentalItem> RentalItems { get; set; }
        public ApplicationUser User { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is Rental rental &&
                   Id == rental.Id &&
                   EndDate == rental.EndDate &&
                   StartDate == rental.StartDate &&
                   RentingDate == rental.RentingDate &&
                   TotalPrice == rental.TotalPrice &&
                   DeliveryCarDealer == rental.DeliveryCarDealer &&
                   RentalItems == rental.RentalItems;

        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(EndDate);
            hash.Add(StartDate);
            hash.Add(RentingDate);
            hash.Add(TotalPrice);
            hash.Add(DeliveryCarDealer);
            hash.Add(RentalItems);
            return hash.ToHashCode();
        }
    }
}