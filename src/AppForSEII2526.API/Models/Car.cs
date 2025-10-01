using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;

namespace AppForSEII2526.API.Models

{
    public class Car {

        public String carClass, Color, Description, Manufacturer, RentalItems, ReviewItems;

        public int Id, QuantityForPurchasing, QuantityForRenting;

        public float PurchasingPrice, RentingPrice;

        public IList<PurchaseItem> purchaseItems;   //Es una lista de PurchaseItems, Lo hemos hecho de tipo Ilist porque para relacionarlo con la clase Purchase necesitas la clase intermedia que es PurchaseItem

        public Car(){ 
        
        }

        public Car(string carClass, string color, string description, string manufacturer, string rentalItems, string reviewItems, int id, int quantityForPurchasing, int quantityForRenting, float purchasingPrice, float rentingPrice)
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

        public override bool Equals(object? obj){
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

        public override int GetHashCode(){

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
