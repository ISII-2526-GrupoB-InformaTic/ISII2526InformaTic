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

        public PurchaseItemDTO(int carId, int purchaseId, int quantity, String car)
        {

            CarId = carId;

            PurchaseId = purchaseId;

            Quantity = quantity;

            Car = car;

        }

        public PurchaseItemDTO(int carId, int purchaseId, int quantity, String car, String fuelType, String fabricante, int precio, string color, string descripcion)
        {

            CarId = carId;

            PurchaseId = purchaseId;

            Quantity = quantity;

            Car = car;

            FuelType = fuelType;
            
            Fabricante = fabricante;
            
            Price = precio;

            Color = color;

            Descripcion = descripcion;

        }


        public int CarId { get; set; }      //Ponemos el get y set para poder modificar luego los valores en la base de datos

        public int PurchaseId { get; set; }


        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        //public PurchaseDTO Purchase { get; set; }   //DABA ERROR DEBIDO A QUE PURCHASE LLAMABA A PURCHASEITEM, Y PURCHASEITEM LLAMABA A USER Y USER LLAMABA PURCHASE Y ASI TODO EL RATO
        public String Car { get; set; }

        public string FuelType { get; set; }

        public string Fabricante { get; set; }

        public int Price { get; set; }

        public string Color { get; set; }

        public string Descripcion { get; set; }

        public override bool Equals(object? obj)
        {

            return obj is PurchaseItemDTO item &&

                CarId == item.CarId &&

                PurchaseId == item.PurchaseId &&

                Quantity == item.Quantity &&

                Car == item.Car;

        }

        public override int GetHashCode()
        {

            return HashCode.Combine(CarId, PurchaseId, Quantity, Car);

        }

    }
}
