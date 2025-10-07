
namespace AppForSEII2526.API.Models
{
    public class PurchaseItem
    {

        public int CarId, PurchaseId, Quantity;

        public Purchase purchase;

        public Car car;

        public PurchaseItem()
        {

        }

        public PurchaseItem(int carId, int purchaseId, int quantity, Car car, Purchase purchase)
        {

            CarId = carId;

            PurchaseId = purchaseId;

            Quantity = quantity;

            this.car = car;

            this.purchase = purchase;

        }

        public override bool Equals(object? obj)
        {

            return obj is PurchaseItem item &&

                CarId == item.CarId &&

                PurchaseId == item.PurchaseId &&

                Quantity == item.Quantity &&

                car == item.car && 
                
                purchase == item.purchase;

        }

        public override int GetHashCode()
        {

            return HashCode.Combine(CarId, PurchaseId, Quantity, car, purchase);

        }



    }


}