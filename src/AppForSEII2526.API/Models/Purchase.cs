namespace AppForSEII2526.API.Models
{
    public class Purchase{

        String DeliveryCarDealer, Name, PaymentMethod, Surname;

        DateTime PurchasingDate;

        float PurchasingPrice;

        int Id;

        public Purchase(){

        }


        public Purchase(string deliveryCarDealer, string name, string paymentMethod, string surname, DateTime purchasingDate, float purchasingPrice, int id)
        {
            DeliveryCarDealer = deliveryCarDealer;
            
            Name = name;
            
            PaymentMethod = paymentMethod;
            
            Surname = surname;
            
            PurchasingDate = purchasingDate;
            
            PurchasingPrice = purchasingPrice;
            
            Id = id;
        
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
