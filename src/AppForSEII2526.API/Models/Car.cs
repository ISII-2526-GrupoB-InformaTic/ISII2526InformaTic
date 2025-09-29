using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;

namespace AppForSEII2526.API.Models

{
    public class Car {

        String carClass, Color, Description, Manufacturer, RentalItems, ReviewItems;

        int Id, QuantityForPurchasing, QuantityForRenting;

        float PurchasingPrice, RentingPrice;

        public Car(){ 
        

        
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
