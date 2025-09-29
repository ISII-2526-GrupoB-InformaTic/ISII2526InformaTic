namespace AppForSEII2526.API.Models
{
    public class Purchase{

        String DeliveryCarDealer, Name, PaymentMethod, Surname;

        DateTime PurchasingDate;

        float PurchasingPrice;

        int Id;

        public Purchase(){


        }

        public override bool Equals(object? obj){
            
            return obj is Purchase purchase &&
                   
                DeliveryCarDealer == purchase.DeliveryCarDealer &&
                   
                Name == purchase.Name &&
                
                PaymentMethod == purchase.PaymentMethod &&
                
                Surname == purchase.Surname &&
                
                PurchasingDate == purchase.PurchasingDate &&
                
                PurchasingPrice == purchase.PurchasingPrice &&
                
                Id == purchase.Id;
        
        }

        public override int GetHashCode(){
        
            return HashCode.Combine(DeliveryCarDealer, Name, PaymentMethod, Surname, PurchasingDate, PurchasingPrice, Id);
        
        }

    }


}
