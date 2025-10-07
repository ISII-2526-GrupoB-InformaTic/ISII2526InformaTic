using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;

namespace AppForSEII2526.API.Models

{
    public class Car
    {

        public String carClass, Color, Description, Manufacturer, ReviewItems;

        public int Id, QuantityForPurchasing, QuantityForRenting;

        public float PurchasingPrice, RentingPrice;

        public Model Model { get; set; }
        public Car()
        {

        }

        public Car(string carClass, string color, string description, string manufacturer, string reviewItems, int id,
            int quantityForPurchasing, int quantityForRenting, float purchasingPrice, float rentingPrice, Model model)
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
                   Model == car.Model;
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
            return hash.ToHashCode();
        }
    }

}
