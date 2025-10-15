using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;

namespace AppForSEII2526.API.Models

{
    public class Car
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
        public String Description {  get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Manufacturer name can't be longer than 100 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String Manufacturer {  get; set; }

        [Required]
        public String ReviewItems {  get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Minimum 1, Maximum 100")]
        public int QuantityForPurchasing {  get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Minimum 1, Maximum 100")]
        public int QuantityForRenting {  get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, 1000000, ErrorMessage = "Minimum 1, Maximum 1000000")]
        [Precision(5, 2)]

        public float PurchasingPrice {  get; set; }
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(1, 1000000, ErrorMessage = "Minimum 1, Maximum 1000000")]
        [Precision(5, 2)]
        public float RentingPrice { get; set; }

        public Model Model { get; set; }
        public IList<RentalItem> RentalItems { get; set; }

        public IList<PurchaseItem> PurchaseItems { get; set; }
        public Car()
        {

        }

        public Car(string carClass, string color, string description, string manufacturer, string reviewItems, int id,
            int quantityForPurchasing, int quantityForRenting, float purchasingPrice, float rentingPrice, Model model, IList<RentalItem> rentalItems, IList<PurchaseItem> purchaseItems)
        {
            this.carClass = carClass;
            Color = color;
            Description = description;
            Manufacturer = manufacturer;
            ReviewItems = reviewItems;
            Id = id;
            QuantityForPurchasing = quantityForPurchasing;
            QuantityForRenting = quantityForRenting;
            PurchasingPrice = purchasingPrice;
            RentingPrice = rentingPrice;
            Model = model;
            RentalItems = rentalItems;
            PurchaseItems = purchaseItems;
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
                   QuantityForPurchasing == car.QuantityForPurchasing &&
                   QuantityForRenting == car.QuantityForRenting &&
                   PurchasingPrice == car.PurchasingPrice &&
                   RentingPrice == car.RentingPrice &&
                   Model == car.Model &&
                   RentalItems == car.RentalItems &&
                   PurchaseItems == car.PurchaseItems;        

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
            hash.Add(QuantityForPurchasing);
            hash.Add(QuantityForRenting);
            hash.Add(PurchasingPrice);
            hash.Add(RentingPrice);
            hash.Add(Model);
            hash.Add(RentalItems);
            hash.Add(PurchaseItems);
            return hash.ToHashCode();
        }
    }

}
