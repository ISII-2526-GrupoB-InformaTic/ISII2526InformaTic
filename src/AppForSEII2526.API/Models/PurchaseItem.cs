
namespace AppForSEII2526.API.Models
{
    public class PurchaseItem{

        int CarId, PurchaseId, Quantity;

        public PurchaseItem(){

        }

        public PurchaseItem(int carId, int purchaseId, int quantity){
            
            CarId = carId;
            
            PurchaseId = purchaseId;
        
            Quantity = quantity;
        
        }

        public override bool Equals(object? obj){

            return obj is PurchaseItem item &&
                   
                CarId == item.CarId &&
                
                PurchaseId == item.PurchaseId &&
                
                Quantity == item.Quantity;
        
        }

        public override int GetHashCode(){
            
            return HashCode.Combine(CarId, PurchaseId, Quantity);
        
        }



    }


}
