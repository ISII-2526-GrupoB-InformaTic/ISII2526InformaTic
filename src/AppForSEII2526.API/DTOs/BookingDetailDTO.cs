
namespace AppForSEII2526.API.DTOs
{
    public class BookingDetailDTO : BookingForCreateDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DeliveryAddress { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime BookingDate { get; set; }

        public IList<BookingItemDTO> BookingItemDTOs { get; set; }

        public float PriceTotal
        {
            get
            {
                return BookingItemDTOs.Sum(bi => bi.Maintenance.Price);
            }
        }

        public float NecessaryDays
        {
            get
            {
                return BookingItemDTOs.Sum(bi => bi.Maintenance.NumberOfDays);
            }
        }

        public string InfoManintenance
        {
            get
            {
                return string.Join("Nombre: ", BookingItemDTOs.Select(bi => bi.Maintenance.Name), ", Precio: ", BookingItemDTOs.Select(bi => bi.Maintenance.Price), ", Dias: ", BookingItemDTOs.Select(bi => bi.Maintenance.NumberOfDays), ", Comentario: ", BookingItemDTOs.Select(bi => bi.Comment), ". ");
            }
        }

        public BookingDetailDTO(string name, string surname, string deliveryAddress, PaymentMethod paymentMethod, DateTime bookingDate, IList<BookingItemDTO> bookingItemDTOs)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            DeliveryAddress = deliveryAddress ?? throw new ArgumentNullException(nameof(deliveryAddress));
            PaymentMethod = paymentMethod;
            BookingDate = bookingDate;
            BookingItemDTOs = bookingItemDTOs;
        }

        public override bool Equals(object? obj)
        {
            return obj is BookingDetailDTO dTO &&
                   Id == dTO.Id &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DeliveryAddress == dTO.DeliveryAddress &&
                   PaymentMethod == dTO.PaymentMethod &&
                   BookingDate == dTO.BookingDate &&
                   EqualityComparer<IList<BookingItemDTO>>.Default.Equals(BookingItemDTOs, dTO.BookingItemDTOs) &&
                   PriceTotal == dTO.PriceTotal &&
                   NecessaryDays == dTO.NecessaryDays &&
                   InfoManintenance == dTO.InfoManintenance;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(DeliveryAddress);
            hash.Add(PaymentMethod);
            hash.Add(BookingDate);
            hash.Add(BookingItemDTOs);
            hash.Add(PriceTotal);
            hash.Add(NecessaryDays);
            hash.Add(InfoManintenance);
            return hash.ToHashCode();
        }
    }
}
