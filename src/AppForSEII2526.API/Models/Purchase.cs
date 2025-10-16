using Microsoft.Data.SqlClient;

namespace AppForSEII2526.API.Models
{
    public class Purchase
    {

        [Required]
        [StringLength(10, ErrorMessage = "Nombre no valido")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String DeliveryCarDealer {get; set;}

        [Required]
        public PaymentMethod PaymentMethod { get; set; }


        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date), Display(Name ="Purchase Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchasingDate {get; set;}


        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Display(Name = "Price for Car")]
        public int PurchasingPrice { get; set; }
        

        [Key]
        public int Id {get; set; }

        public IList<PurchaseItem> purchaseItems { get; set; }   //Es una lista de PurchaseItems, Lo hemos hecho de tipo Ilist porque para relacionarlo con la clase Car necesitas la clase intermedia que es PurchaseItem
        public ApplicationUser User { get; set; }


        public Purchase()
        {

        }


        public Purchase(string deliveryCarDealer, string paymentMethod, DateTime purchasingDate, float purchasingPrice, int id, IList<PurchaseItem> purchaseItem, ApplicationUser User)
        {
            DeliveryCarDealer = deliveryCarDealer;
            PaymentMethod = paymentMethod;
            PurchasingDate = purchasingDate;
            PurchasingPrice = purchasingPrice;
            Id = id;
            purchaseItems = purchaseItem;
            this.User = User;

        }

        public override bool Equals(object? obj)
        {

            return obj is Purchase purchase &&

                DeliveryCarDealer == purchase.DeliveryCarDealer &&

                PaymentMethod == purchase.PaymentMethod &&

                PurchasingDate == purchase.PurchasingDate &&

                PurchasingPrice == purchase.PurchasingPrice &&

                Id == purchase.Id &&

                purchaseItems == purchase.purchaseItems &&
                
                this.User == purchase.User;

        }

        public override int GetHashCode()
        {

            return HashCode.Combine(DeliveryCarDealer, PaymentMethod, PurchasingDate, PurchasingPrice, Id, purchaseItems, User);

        }

    }


}
