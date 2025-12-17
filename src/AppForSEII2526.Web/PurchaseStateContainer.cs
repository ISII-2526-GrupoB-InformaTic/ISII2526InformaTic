using AppForSEII2526.Web.API;


namespace AppForSEII2526.Web
{
    public class PurchaseStateContainer
    {

        public PurchaseForCreateDTO Purchase { get; private set; } = new PurchaseForCreateDTO()
        {
            PurchaseItemDTO = new List<PurchaseItemDTO>()
        };

        public decimal TotalPrice
        {
            get
            {
                return Convert.ToDecimal(this.Purchase.TotalPrice);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        //BUSCO LOS COCHES PARA HACER LA COMPRA
        public void AddCarToPurchase(CarForPurchasingDTO car)
        {
            if (!Purchase.PurchaseItemDTO.Any(pi => pi.CarId == car.Id))
            {
                Purchase.PurchaseItemDTO.Add(new PurchaseItemDTO()
                {
                    CarId = car.Id,
                    Car = car.Model,
                    Color = car.Color,
                    Descripcion = car.Descripcion,
                    FuelType = car.FuelType,
                    Fabricante = car.Manufacture,
                    Price = car.PurchasingPrice,
                }
                );
            }
        }

        public void RemovePurchaseItemToPurchase(PurchaseItemDTO item)
        {
            Purchase.PurchaseItemDTO.Remove(item);

        }

        public void ClearPurchasingCar()
        {
            Purchase.PurchaseItemDTO.Clear();
        }

        public void PurchaseProcessed()
        {
            Purchase = new PurchaseForCreateDTO()
            {
                PurchaseItemDTO = new List<PurchaseItemDTO>()
            };
        }

    }
}
