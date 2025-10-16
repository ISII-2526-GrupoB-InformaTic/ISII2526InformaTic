
namespace AppForSEII2526.API.Models
{
    public class PurchaseItem
    {

        public int CarId { get; set; }      //Ponemos el get y set para poder modificar luego los valores en la base de datos
            
        public int PurchaseId { get; set; }


        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        public Purchase purchase { get; set; }

        public Car car { get; set; }

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