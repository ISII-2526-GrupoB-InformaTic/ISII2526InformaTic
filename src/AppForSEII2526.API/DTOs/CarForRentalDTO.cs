namespace AppForSEII2526.API.DTOs
{
    public class CarForRentalDTO
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String carClass { get; set; }

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
        public String ReviewItems { get; set; }

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

        [Required]
        public string FuelType { get; set; }

        [Required]
        public string EngDisplacement { get; set; }

        [Required]
        public string RimSize { get; set; }

        public IList<MaintenanceType> MaintenanceTypes { get; set; }
        public CarForRentalDTO()
        {

        }

        public CarForRentalDTO(int id, string carClass, string color, string description, string manufacturer, string reviewItems, 
            int quantityForRenting, int rentingPrice, Model model, IList<RentalItemDTO> rentalItems)
        {
            this.carClass = carClass;
            Color = color;
            Description = description;
            Manufacturer = manufacturer;
            ReviewItems = reviewItems;
            Id = id;
            QuantityForRenting = quantityForRenting;
            RentingPrice = rentingPrice;
            Model = model;
            RentalItems = rentalItems;
        }

        public CarForRentalDTO(int id, string color, string manufacturer, int rentingPrice, string fuelType, Model model)
        {
            Id = id;
            Color = color;
            Manufacturer = manufacturer;
            RentingPrice = rentingPrice;
            FuelType = fuelType;
            Model = model;
        }

        public override bool Equals(object? obj)
        {
            return obj is Car car &&
                   carClass == car.carClass &&
                   Color == car.Color &&
                   Description == car.Description &&
                   Manufacturer == car.Manufacturer &&
                   ReviewItems == car.ReviewItems &&
                   Id == car.Id &&
                   QuantityForRenting == car.QuantityForRenting &&
                   RentingPrice == car.RentingPrice &&
                   Model == car.Model &&
                   RentalItems == car.RentalItems;

        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(carClass);
            hash.Add(Color);
            hash.Add(Description);
            hash.Add(Manufacturer);
            hash.Add(ReviewItems);
            hash.Add(Id);
            hash.Add(QuantityForRenting);
            hash.Add(RentingPrice);
            hash.Add(Model);
            hash.Add(RentalItems);
            return hash.ToHashCode();
        }
    }
}
