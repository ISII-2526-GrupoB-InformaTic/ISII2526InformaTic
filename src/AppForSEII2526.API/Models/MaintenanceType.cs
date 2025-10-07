using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class MaintenanceType
    {
        public MaintenanceType()
        {

        }
        public MaintenanceType(int Id, string Type) : base()
        {
            this.Id = Id;
            this.Type = Type;

        }

        [Key]
        public int Id { get; set; }
        public string Type { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is MaintenanceType maintenancetype &&
                this.Id == maintenancetype.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }
    }
}