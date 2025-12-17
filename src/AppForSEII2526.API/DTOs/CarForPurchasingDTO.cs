using System.Drawing;

namespace AppForSEII2526.API.DTOs
{
    public class CarForPurchasingDTO    //DTO para enseñar los datos de los coches disponibles para comprar (DTO del select)
    {

       /* public CarForPurchasingDTO(int id, String modelo, String Color, String FuelType, String Manufacture, int PurchasingPrice)
        {

            Id = id;

           this.modelo = modelo;

            color = Color;

            fuelType = FuelType;

            manufacture = Manufacture;

            purchasingPrice = PurchasingPrice;

        }*/

        public CarForPurchasingDTO(int id, String Model,String Color, String Description, String FuelType, String Manufacture, int PurchasingPrice)
        {

            Id = id;

            model = Model;

            color = Color;

            descripcion = Description;

            fuelType = FuelType;

            manufacture = Manufacture;

            purchasingPrice = PurchasingPrice;

        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters. ")]
        public String model { get; set; }


       // public String modelo { get; set; }

        public String color { get; set; }

        public String descripcion { get; set; }

        public String fuelType { get; set; }

        public String manufacture { get; set; }

        public int purchasingPrice { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CarForPurchasingDTO dTO &&
                   Id == dTO.Id &&
                   model == dTO.model &&
                   color == dTO.color &&
                   fuelType == dTO.fuelType &&
                   manufacture == dTO.manufacture &&
                   purchasingPrice == dTO.purchasingPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, model, color, fuelType, manufacture, purchasingPrice);
        }
    }
}
