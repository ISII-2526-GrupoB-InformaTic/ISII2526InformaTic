
namespace AppForSEII2526.API.DTOs
{
    public class PurchaseForCreateDTO   //DTO para enseñar los datos de los coches seleccionados por el cliente (DTO del create)
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your Surname")]
        public string Surname { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string DeliveryAddress { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public int Quantity { get; set; }

        public IList<PurchaseItemDTO> PurchaseItemDTO { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }

        public int Price { get; set; }

        [Display(Name = "Total Price")]
        [JsonPropertyName("TotalPrice")]
        public int TotalPrice
        {
            get
            {
                return (Price * Quantity);
            }
        }


        public PurchaseForCreateDTO(string name, string surname, string deliveryAddress,
            PaymentMethod paymentMethod, DateTime startDate, IList<PurchaseItemDTO> purchaseItemDTOs)
        {
            Name = name;
            Surname = surname;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            PurchaseDate = startDate;
            PurchaseItemDTO = purchaseItemDTOs;
            
        }
        public PurchaseForCreateDTO()
        {
            PurchaseItemDTO = new List<PurchaseItemDTO>();
        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseForCreateDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   Quantity == dTO.Quantity &&
                   PurchaseItemDTO.SequenceEqual(dTO.PurchaseItemDTO) &&
                   PurchaseDate == dTO.PurchaseDate &&
                   Price == dTO.Price &&
                   TotalPrice == dTO.TotalPrice;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            hash.Add(PaymentMethod);
            hash.Add(Quantity);
            hash.Add(PurchaseItemDTO);
            hash.Add(PurchaseDate);
            hash.Add(Price);
            hash.Add(TotalPrice);
            return hash.ToHashCode();
        }

    }

}
