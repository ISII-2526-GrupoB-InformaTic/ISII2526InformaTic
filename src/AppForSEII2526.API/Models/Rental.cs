namespace AppForSEII2526.API.Models
{
    public class Rental
    {
        public Rental()
        {

        }

        public Rental(int id, string name, string surname, DateTime endDate, DateTime startDate, DateTime rentingDate,
            int totalPrice, string deliveryCarDealer)
        {
            Id = id;
            Name = name;
            Surname = surname;
            EndDate = endDate;
            StartDate = startDate;
            RentingDate = rentingDate;
            TotalPrice = totalPrice;
            DeliveryCarDealer = deliveryCarDealer;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime RentingDate { get; set; }
        public int TotalPrice { get; set; }
        public string DeliveryCarDealer { get; set; }
        //public IList<RentalItem> RentalItems { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is Rental rental &&
                   Id == rental.Id &&
                   Name == rental.Name &&
                   Surname == rental.Surname &&
                   EndDate == rental.EndDate &&
                   StartDate == rental.StartDate &&
                   RentingDate == rental.RentingDate &&
                   TotalPrice == rental.TotalPrice &&
                   DeliveryCarDealer == rental.DeliveryCarDealer;

        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Name);
            hash.Add(Surname);
            hash.Add(EndDate);
            hash.Add(StartDate);
            hash.Add(RentingDate);
            hash.Add(TotalPrice);
            hash.Add(DeliveryCarDealer);

            return hash.ToHashCode();
        }
    }
}