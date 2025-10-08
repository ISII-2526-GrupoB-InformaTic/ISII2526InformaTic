using System.ComponentModel.DataAnnotations;
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
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, 1000000, ErrorMessage = "Minimum 1, Maximum 1000000")]
        [Precision(5,2)]
        public int TotalPrice { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "DeliveryCarDealer can be neither longer than 50 characters nor shorter than 10.", MinimumLength=10)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
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