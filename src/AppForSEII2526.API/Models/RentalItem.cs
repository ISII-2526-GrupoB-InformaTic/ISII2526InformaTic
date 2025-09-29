
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
        [Key]
        public int CarId { get; set; }
        public int Quantity { get; set; }
        [Key]
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is RentalItem item &&
                   CarId == item.CarId &&
                   Quantity == item.Quantity &&
                   RentalId == item.RentalId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarId, Quantity, RentalId);
        }
    }
}
