using System.Drawing;

namespace AppForSEII2526.API.DTOs
{
    public class CarForPurchasingDTO    //DTO para enseñar los datos de los coches disponibles para comprar (DTO del select)
    {

        public CarForPurchasingDTO(int id, Model Model,String Color, String FuelType, String Manufacture, int PurchasingPrice)
        {

            Id = id;

            model = Model;

            color = Color;

            fuelType = FuelType;

            manufacture = Manufacture;

            purchasingPrice = PurchasingPrice;

        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Title name cannot be longer than 50 characters. ")]
        public Model model { get; set; }

        public String color { get; set; }

        public String fuelType { get; set; }

        public String manufacture { get; set; }

        public int purchasingPrice { get; set; }

    }
}
