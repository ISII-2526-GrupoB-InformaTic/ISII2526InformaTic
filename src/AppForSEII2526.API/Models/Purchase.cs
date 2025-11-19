using Microsoft.Data.SqlClient;

namespace AppForSEII2526.API.Models
{
    public class Purchase
    {
        internal int purchasing;

        [Required]
        [StringLength(10, ErrorMessage = "Nombre no valido")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String DeliveryCarDealer { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }


        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name = "Purchase Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }


        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Price for Car")]
        public int PurchasingPrice { get; set; }

        [Required]
        public int TotalPrice { get; set; }


        [Key]
        public int Id { get; set; }

        public IList<PurchaseItem> purchaseItems { get; set; }   //Es una lista de PurchaseItems, Lo hemos hecho de tipo Ilist porque para relacionarlo con la clase Car necesitas la clase intermedia que es PurchaseItem
        public ApplicationUser User { get; set; }
        

        public Purchase()
        {

        }


        public Purchase(string deliveryCarDealer, PaymentMethod paymentMethod, DateTime purchasingDate, int purchasingPrice, int id, IList<PurchaseItem> purchaseItem, ApplicationUser User)
        {
            DeliveryCarDealer = deliveryCarDealer;
            PaymentMethod = paymentMethod;
            PurchaseDate = purchasingDate;
            PurchasingPrice = purchasingPrice;
            Id = id;
            purchaseItems = purchaseItem;
            this.User = User;

        }

        public override bool Equals(object? obj)
        {
            return obj is Purchase purchase &&
                   purchasing == purchase.purchasing &&
                   DeliveryCarDealer == purchase.DeliveryCarDealer &&
                   PaymentMethod == purchase.PaymentMethod &&
                   PurchaseDate == purchase.PurchaseDate &&
                   PurchasingPrice == purchase.PurchasingPrice &&
                   TotalPrice == purchase.TotalPrice &&
                   Id == purchase.Id &&
                   EqualityComparer<IList<PurchaseItem>>.Default.Equals(purchaseItems, purchase.purchaseItems) &&
                   EqualityComparer<ApplicationUser>.Default.Equals(User, purchase.User);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(purchasing);
            hash.Add(DeliveryCarDealer);
            hash.Add(PaymentMethod);
            hash.Add(PurchaseDate);
            hash.Add(PurchasingPrice);
            hash.Add(TotalPrice);
            hash.Add(Id);
            hash.Add(purchaseItems);
            hash.Add(User);
            return hash.ToHashCode();
        }
    }


}
