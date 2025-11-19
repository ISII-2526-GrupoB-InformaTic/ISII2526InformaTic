
namespace AppForSEII2526.API.DTOs
{
    public class CarForRentalDTO
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Color can't be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String Color { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String Description { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Manufacturer name can't be longer than 100 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String Manufacturer { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Minimum 1, Maximum 100")]
        public int QuantityForRenting { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, 1000000, ErrorMessage = "Minimum 1, Maximum 1000000")]
        [Precision(5, 2)]
        public int RentingPrice { get; set; }

        public Model Model { get; set; }
        public IList<RentalItemDTO> RentalItems { get; set; }

        public string FuelType { get; set; }

        public CarForRentalDTO()
        {

        }


        public CarForRentalDTO(int id,string description, string color, string manufacturer, int rentingPrice,int quantity, string fuelType, Model model)
        {
            Id = id;
            Description = description;
            Color = color;
            Manufacturer = manufacturer;
            RentingPrice = rentingPrice;
            FuelType = fuelType;
            Model = model;
            QuantityForRenting = quantity;
        }

        public override bool Equals(object? obj)
        {
            return obj is CarForRentalDTO dTO &&
                   Id == dTO.Id &&
                   Color == dTO.Color &&
                   Description == dTO.Description &&
                   Manufacturer == dTO.Manufacturer &&
                   QuantityForRenting == dTO.QuantityForRenting &&
                   RentingPrice == dTO.RentingPrice &&
                   EqualityComparer<Model>.Default.Equals(Model, dTO.Model) &&
                   RentalItems.SequenceEqual(dTO.RentalItems) &&
                   FuelType == dTO.FuelType;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Color);
            hash.Add(Description);
            hash.Add(Manufacturer);
            hash.Add(QuantityForRenting);
            hash.Add(RentingPrice);
            hash.Add(Model);
            hash.Add(RentalItems);
            hash.Add(FuelType);
            return hash.ToHashCode();
        }
    }
}
