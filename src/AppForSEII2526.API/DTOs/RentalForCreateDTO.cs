
namespace AppForSEII2526.API.DTOs
{
    public class RentalForCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Name and Surname must have at least 10 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Name and Surname must have at least 10 characters")]
        public string Surname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Delivery address must have at least 10 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string DeliveryAddress { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public int Quantity { get; set; }

        public IList<RentalItemDTO> rentalItems { get; set; }

        public RentalForCreateDTO(string name, string surname, string deliveryAddress,
            PaymentMethod paymentMethod, int quantity)
        {
            Name = name;
            Surname = surname;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            Quantity = quantity;
        }
        public RentalForCreateDTO()
        {
            rentalItems = new List<RentalItemDTO>();
        }

        public override bool Equals(object? obj)
        {
            return obj is RentalForCreateDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   Quantity == dTO.Quantity &&
                   EqualityComparer<IList<RentalItemDTO>>.Default.Equals(rentalItems, dTO.rentalItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, DeliveryAddress, PaymentMethod, Quantity, rentalItems);
        }
    }
}
