using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class Maintenance
    {
        public Maintenance()
        {

        }
        public Maintenance(int Id, string Name, int NumberOfDays, float Price) : base()
        {
            this.Id = Id;
            this.Name = Name;
            this.NumberOfDays = NumberOfDays;
            this.Price = Price;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
        public float Price { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is Maintenance maintenance &&
                this.Id == maintenance.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}