
namespace AppForSEII2526.API.DTOs
{
    public class PurchaseForDetailsDTO : PurchaseForDetailsDTO
    //DTO para enseñar los datos de las compras realizadas (DTO del details)
    {

       
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String Direccion { get; set; }
        public DateTime dateTime { get; set; }
        public int Purchasing { get; set; }
        [Required]
        public IList<PurchaseItemDTO> Cantidad { get; set; }


        public PurchaseForDetailsDTO( String Nombre, String Apellidos, String Direccion, DateTime DateTime, int purchasing, IList<PurchaseItemDTO> purchaseItems)
        {

            Nombre = Nombre;
            Apellidos = Apellidos;
            Direccion = Direccion;
            dateTime = DateTime;
            Purchasing = purchasing;
            Cantidad = purchaseItems;

        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseForDetailsDTO dTO &&
                   Nombre == dTO.Nombre &&
                   Apellidos == dTO.Apellidos &&
                   Direccion == dTO.Direccion &&
                   dateTime == dTO.dateTime &&
                   Purchasing == dTO.Purchasing &&
                   EqualityComparer<IList<PurchaseItemDTO>>.Default.Equals(Cantidad, dTO.Cantidad);
        }

        public override int GetHashCode()
        {
            
            HashCode hash = new HashCode();
            hash.Add(Nombre);
            hash.Add(Apellidos);
            hash.Add(Direccion);
            hash.Add(dateTime);
            hash.Add(Purchasing);
            hash.Add(Cantidad);
            return hash.ToHashCode();

        }

    }
}
