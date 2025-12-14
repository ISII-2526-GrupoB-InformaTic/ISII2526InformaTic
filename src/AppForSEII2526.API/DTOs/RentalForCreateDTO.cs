
namespace AppForSEII2526.API.DTOs
{
    public class RentalForCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        [StringLength(50, ErrorMessage = "Surname can't be longer than 5 characters")]
        public string Surname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [StringLength(50,  ErrorMessage = "Delivery address can't be longer than 50 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string DeliveryAddress { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public int Quantity { get; set; }

        public IList<RentalItemDTO> RentalItems { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentingDate { get; set; }

        public int Price { get; set; }
        private int NumberOfDays
        {
            get
            {
                return (EndDate - StartDate).Days;
            }
        }

        [Display(Name = "Total Price")]
        [JsonPropertyName("TotalPrice")]
        public int TotalPrice
        {
            get
            {
                return (Price * NumberOfDays) / 2;
            }
        }
        public string Username { get; set; }
        public RentalForCreateDTO(string name, string surname, string deliveryAddress,
            PaymentMethod paymentMethod, DateTime startDate, DateTime endDate, IList<RentalItemDTO> rentalItem,string username)
        {
            Name = name;
            Surname = surname;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            RentalItems = rentalItem;
            StartDate = startDate;
            EndDate = endDate;
            Username = username;
        }
        public RentalForCreateDTO()
        {
            RentalItems = new List<RentalItemDTO>();
        }

        public override bool Equals(object? obj)
        {
            return obj is RentalForCreateDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   Quantity == dTO.Quantity &&
                   RentalItems.SequenceEqual(dTO.RentalItems) &&
                   EndDate == dTO.EndDate &&
                   StartDate == dTO.StartDate &&
                   RentingDate == dTO.RentingDate &&
                   Price == dTO.Price &&
                   NumberOfDays == dTO.NumberOfDays &&
                   TotalPrice == dTO.TotalPrice;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            hash.Add(PaymentMethod);
            hash.Add(Quantity);
            hash.Add(RentalItems);
            hash.Add(EndDate);
            hash.Add(StartDate);
            hash.Add(RentingDate);
            hash.Add(Price);
            hash.Add(NumberOfDays);
            hash.Add(TotalPrice);
            return hash.ToHashCode();
        }
    }
}
