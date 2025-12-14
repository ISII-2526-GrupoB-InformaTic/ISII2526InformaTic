

namespace AppForSEII2526.API.DTOs
{
    public class RentalDetailDTO : RentalForCreateDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DeliveryAddress { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

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

        private int NumberOfDays
        {
            get
            {
                return (EndDate - StartDate).Days;
            }
        }

        [Display(Name = "Total Price")]
        [JsonPropertyName("TotalPrice")]
        public double TotalPrice
        {
            get
            {
                return (Price * NumberOfDays)/2;
            }
        }
        

        public RentalDetailDTO(string name, string surname, string deliveryAddress,PaymentMethod paymentMethod,
            DateTime startDate, DateTime endDate,DateTime rentingDate, IList<RentalItemDTO> rentalItems, string username) :base (name,surname,deliveryAddress,paymentMethod,startDate,endDate,rentalItems,username)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress));
            PaymentMethod = paymentMethod;
            StartDate = startDate;
            EndDate = endDate;
            RentingDate = rentingDate;

            DateTime.SpecifyKind(StartDate, DateTimeKind.Local);
            DateTime.SpecifyKind(EndDate, DateTimeKind.Local);
            DateTime.SpecifyKind(RentingDate, DateTimeKind.Local);
        }

        public RentalDetailDTO(int id, string name, string surname, string deliveryAddress, PaymentMethod paymentMethod,
        DateTime startDate, DateTime endDate, DateTime rentingDate, IList<RentalItemDTO> rentalItems, string username) : base(name, surname, deliveryAddress, paymentMethod, startDate, endDate, rentalItems, username)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress));
            PaymentMethod = paymentMethod;
            StartDate = startDate;
            EndDate = endDate;
            RentingDate = rentingDate;

            DateTime.SpecifyKind(StartDate, DateTimeKind.Local);
            DateTime.SpecifyKind(EndDate, DateTimeKind.Local);
            DateTime.SpecifyKind(RentingDate, DateTimeKind.Local);
        }
        public override bool Equals(object? obj)
        {
            return obj is RentalDetailDTO dTO &&
                   base.Equals(obj) &&
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
                   TotalPrice == dTO.TotalPrice &&
                   Id == dTO.Id &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   EndDate == dTO.EndDate &&
                   StartDate == dTO.StartDate &&
                   RentingDate == dTO.RentingDate &&
                   NumberOfDays == dTO.NumberOfDays &&
                   TotalPrice == dTO.TotalPrice;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
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
            hash.Add(TotalPrice);
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            hash.Add(PaymentMethod);
            hash.Add(EndDate);
            hash.Add(StartDate);
            hash.Add(RentingDate);
            hash.Add(NumberOfDays);
            hash.Add(TotalPrice);
            return hash.ToHashCode();
        }
    }
}
