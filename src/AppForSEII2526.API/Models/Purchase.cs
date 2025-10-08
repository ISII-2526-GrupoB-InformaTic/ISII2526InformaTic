namespace AppForSEII2526.API.Models
{
    public class Purchase
    {

        public String DeliveryCarDealer, PaymentMethod;

        public DateTime PurchasingDate;

        public float PurchasingPrice;
        [Key]
        public int Id;

        public IList<PurchaseItem> purchaseItems;   //Es una lista de PurchaseItems, Lo hemos hecho de tipo Ilist porque para relacionarlo con la clase Car necesitas la clase intermedia que es PurchaseItem
        public ApplicationUser User { get; set; }
        public Purchase()
        {

        }


        public Purchase(string deliveryCarDealer, string paymentMethod, DateTime purchasingDate, float purchasingPrice, int id, IList<PurchaseItem> purchaseItem)
        {
            DeliveryCarDealer = deliveryCarDealer;
            PaymentMethod = paymentMethod;
            PurchasingDate = purchasingDate;
            PurchasingPrice = purchasingPrice;
            Id = id;
            purchaseItems = purchaseItem;

        }

        public override bool Equals(object? obj)
        {

            return obj is Purchase purchase &&

                DeliveryCarDealer == purchase.DeliveryCarDealer &&

                PaymentMethod == purchase.PaymentMethod &&

                PurchasingDate == purchase.PurchasingDate &&

                PurchasingPrice == purchase.PurchasingPrice &&

                Id == purchase.Id &&

                purchaseItems == purchase.purchaseItems;

        }

        public override int GetHashCode()
        {

            return HashCode.Combine(DeliveryCarDealer, PaymentMethod, PurchasingDate, PurchasingPrice, Id, purchaseItems);

        }

    }


}
