namespace AppForSEII2526.API.Models
{
    public class Car
    {

        public Car()
        {

        }

        public Car(string carClass, string color, string description, string manufacturer, IList<RentalItem> rentalItems, string reviewItems, int id, int quantityForPurchasing, int quantityForRenting, float purchasingPrice, float rentingPrice)
        {
            this.carClass = carClass;

            Color = color;

            Description = description;

            Manufacturer = manufacturer;

            RentalItems = rentalItems;

            ReviewItems = reviewItems;

            Id = id;

            QuantityForPurchasing = quantityForPurchasing;

            QuantityForRenting = quantityForRenting;

            PurchasingPrice = purchasingPrice;

            RentingPrice = rentingPrice;

        }

        [Key]
        public int Id { get; set; }
        String carClass, Color, Description, Manufacturer, ReviewItems;

        int QuantityForPurchasing, QuantityForRenting;

        float PurchasingPrice, RentingPrice;

        IList<RentalItem> RentalItems { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Car car &&

                carClass == car.carClass &&

                Color == car.Color &&

                Description == car.Description &&

                Manufacturer == car.Manufacturer &&

                RentalItems == car.RentalItems &&

                ReviewItems == car.ReviewItems &&

                Id == car.Id &&

                QuantityForPurchasing == car.QuantityForPurchasing &&

                QuantityForRenting == car.QuantityForRenting &&

                PurchasingPrice == car.PurchasingPrice &&

                RentingPrice == car.RentingPrice;

        }

        public override int GetHashCode()
        {

            HashCode hash = new HashCode();

            hash.Add(carClass);

            hash.Add(Color);

            hash.Add(Description);

            hash.Add(Manufacturer);

            hash.Add(RentalItems);

            hash.Add(ReviewItems);

            hash.Add(Id);

            hash.Add(QuantityForPurchasing);

            hash.Add(QuantityForRenting);

            hash.Add(PurchasingPrice);

            hash.Add(RentingPrice);

            return hash.ToHashCode();

        }


    }
}
