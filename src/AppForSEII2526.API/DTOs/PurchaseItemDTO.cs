namespace AppForSEII2526.API.DTOs
{
    public class PurchaseItemDTO    //Estoy consiguiendo con este DTO la relacion entre Purchase y Car a traves de PurchaseItem
    {

        public PurchaseItemDTO()
        {

        }

        public PurchaseItemDTO(int carId, int purchaseId, int quantity)
        {

            CarId = carId;

            PurchaseId = purchaseId;

            Quantity = quantity;

        }


        public int CarId { get; set; }      //Ponemos el get y set para poder modificar luego los valores en la base de datos

        public int PurchaseId { get; set; }


        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        public PurchaseDTO Purchase { get; set; }
        public CarForPurchasingDTO Car { get; set; }

        public override bool Equals(object? obj)
        {

            return obj is PurchaseItemDTO item &&

                CarId == item.CarId &&

                PurchaseId == item.PurchaseId &&

                Quantity == item.Quantity &&

                EqualityComparer<PurchaseDTO>.Default.Equals(Purchase, item.Purchase) &&

                EqualityComparer<CarForPurchasingDTO>.Default.Equals(Car, item.Car);




        }

        public override int GetHashCode()
        {

            return HashCode.Combine(CarId, PurchaseId, Quantity, Purchase, Car);

        }

    }
}
