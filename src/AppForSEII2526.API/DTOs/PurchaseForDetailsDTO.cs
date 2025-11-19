

namespace AppForSEII2526.API.DTOs
{
    public class PurchaseForDetailsDTO : PurchaseForCreateDTO
    //DTO para enseñar los datos de las compras realizadas (DTO del details)
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DeliveryAddress { get; set; }

        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }


        public PurchaseForDetailsDTO(string name, string surname, string deliveryAddress,PaymentMethod paymentMethod,
            DateTime purchaseDate, IList<PurchaseItemDTO> purchaseItemDTOs) :base (name,surname,deliveryAddress,paymentMethod,purchaseDate,purchaseItemDTOs)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress));
            PaymentMethod = paymentMethod;
            PurchaseDate = purchaseDate;


        }

        public override bool Equals(object? obj)
        {
            return obj is PurchaseForDetailsDTO dTO &&
                   base.Equals(obj) &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   Quantity == dTO.Quantity &&
                   PurchaseItemDTO.SequenceEqual(dTO.PurchaseItemDTO) &&
                   PurchaseDate == dTO.PurchaseDate &&
                   Price == dTO.Price &&
                   TotalPrice == dTO.TotalPrice &&
                   Id == dTO.Id &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PurchaseDate == dTO.PurchaseDate;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            hash.Add(PaymentMethod);
            hash.Add(Quantity);
            hash.Add(PurchaseItemDTO);
            hash.Add(PurchaseDate);
            hash.Add(Price);
            hash.Add(TotalPrice);
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            hash.Add(PurchaseDate);
            return hash.ToHashCode();
        }

    }
}
