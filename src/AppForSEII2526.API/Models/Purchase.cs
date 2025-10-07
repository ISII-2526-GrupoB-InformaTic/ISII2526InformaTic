namespace AppForSEII2526.API.Models
{
    public class Purchase
    {

        public String DeliveryCarDealer, Name, PaymentMethod, Surname;

        public DateTime PurchasingDate;

        public float PurchasingPrice;

        public int Id;

        public IList<PurchaseItem> purchaseItems;   //Es una lista de PurchaseItems, Lo hemos hecho de tipo Ilist porque para relacionarlo con la clase Car necesitas la clase intermedia que es PurchaseItem

        public Purchase()
        {

        }


        public Purchase(string deliveryCarDealer, string name, string paymentMethod, string surname, DateTime purchasingDate, float purchasingPrice, int id)
        {
            DeliveryCarDealer = deliveryCarDealer;

            Name = name;

            PaymentMethod = paymentMethod;

            Surname = surname;

            PurchasingDate = purchasingDate;

            PurchasingPrice = purchasingPrice;

            Id = id;

        }

        public override bool Equals(object? obj)
        {

            return obj is Purchase purchase &&

                DeliveryCarDealer == purchase.DeliveryCarDealer &&

                Name == purchase.Name &&

                PaymentMethod == purchase.PaymentMethod &&

                Surname == purchase.Surname &&

                PurchasingDate == purchase.PurchasingDate &&

                PurchasingPrice == purchase.PurchasingPrice &&

                Id == purchase.Id;

        }

        public override int GetHashCode()
        {

            return HashCode.Combine(DeliveryCarDealer, Name, PaymentMethod, Surname, PurchasingDate, PurchasingPrice, Id);

        }

    }


}
