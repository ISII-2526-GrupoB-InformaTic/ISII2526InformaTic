
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

            public int CarId { get; set; }
            [Required]
            [Range(1, 100, ErrorMessage = "Minimum 1, Maximum 100")]
            public int Quantity { get; set; }
            public int RentalId { get; set; }
            public RentalDTO Rental { get; set; }
            public CarForRentalDTO Car { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is RentalItemDTO dTO &&
                   CarId == dTO.CarId &&
                   Quantity == dTO.Quantity &&
                   RentalId == dTO.RentalId &&
                   EqualityComparer<RentalDTO>.Default.Equals(Rental, dTO.Rental) &&
                   EqualityComparer<CarForRentalDTO>.Default.Equals(Car, dTO.Car);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarId, Quantity, RentalId, Rental, Car);
        }
    }
    }
