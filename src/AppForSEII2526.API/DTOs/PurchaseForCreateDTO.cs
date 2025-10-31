
namespace AppForSEII2526.API.DTOs
{
    public class PurchaseForCreateDTO   //DTO para enseñar los datos de los coches seleccionados por el cliente (DTO del create)
    {
        public String color { get; set; }
        public String descripcion { get; set; }
        public int precio { get; set; }

        [Required]
        public PaymentMethod paymentMethod { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Nombre no valido")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String nombre { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Apellido no valido")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String apellido { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Direccion no valido")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]+[0-100]*$")]
        public String direccion { get; set; }
        public IList<PurchaseItemDTO> purchaseItems { get; set; }

        public PurchaseForCreateDTO(String Color, String Descripcion, int Precio, String Nombre, String Apellido, String Direccion, PaymentMethod PaymentMethod, IList<PurchaseItemDTO> purchaseItems)
        {
            color = Color;
            descripcion = Descripcion;
            precio = Precio;
            nombre = Nombre;
            apellido = Apellido;
            direccion = Direccion;
            paymentMethod = PaymentMethod;
            this.purchaseItems = purchaseItems;


        }

        public PurchaseForCreateDTO()
        {

            purchaseItems = new List<PurchaseItemDTO>();

        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseForCreateDTO dTO &&
                   color == dTO.color &&
                   descripcion == dTO.descripcion &&
                   precio == dTO.precio &&
                   paymentMethod == dTO.paymentMethod &&
                   nombre == dTO.nombre &&
                   apellido == dTO.apellido &&
                   direccion == dTO.direccion &&
                   EqualityComparer<IList<PurchaseItemDTO>>.Default.Equals(purchaseItems, dTO.purchaseItems);
        }


        public override int GetHashCode()
        {
            
            HashCode hash = new HashCode();
            hash.Add(color);
            hash.Add(descripcion);
            hash.Add(precio);
            hash.Add(paymentMethod);
            hash.Add(nombre);
            hash.Add(apellido);
            hash.Add(direccion);
            hash.Add(purchaseItems);
            return hash.ToHashCode();

        }

    }

}
