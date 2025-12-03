

namespace AppForSEII2526.API.DTOs
{
    public class RentalItemDTO
    {
            public RentalItemDTO() { }

            public RentalItemDTO(int carId, int quantity, int rentalId)
            {
                CarId = carId;
                Quantity = quantity;
                RentalId = rentalId;
            }
            public RentalItemDTO(int carId, int quantity, int rentalId,int rentingPrice, string car)
            {
                CarId = carId;
                Quantity = quantity;
                RentalId = rentalId;
                Car = car;
                RentingPrice = rentingPrice;
            }
        public int CarId { get; set; }
            [Required]
            [Range(1, 100, ErrorMessage = "Minimum 1, Maximum 100")]
            public int Quantity { get; set; }
            public int RentalId { get; set; }
            public int RentingPrice { get; set; }
            public string Car { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is RentalItemDTO dTO &&
                   CarId == dTO.CarId &&
                   Quantity == dTO.Quantity &&
                   RentalId == dTO.RentalId &&
                   RentingPrice == dTO.RentingPrice &&
                   Car == dTO.Car;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarId, Quantity, RentalId, RentingPrice, Car);
        }
    }
    }
