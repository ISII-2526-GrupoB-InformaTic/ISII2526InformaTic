

namespace AppForSEII2526.API.Models
{
    public class RentalItem
    {
        public RentalItem() { }

        public RentalItem(int carId, int quantity, int rentalId)
        {
            CarId = carId;
            Quantity = quantity;
            RentalId = rentalId;
        }

        public int CarId { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Minimum 1, Maximum 100")]
        public int Quantity { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public Car Car { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is RentalItem item &&
                   CarId == item.CarId &&
                   Quantity == item.Quantity &&
                   RentalId == item.RentalId &&
                   EqualityComparer<Rental>.Default.Equals(Rental, item.Rental);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarId, Quantity, RentalId, Rental);
        }
    }
}