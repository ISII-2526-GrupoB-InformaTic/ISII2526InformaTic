using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class MaintenanceType
    {
        public MaintenanceType()
        {

        }
        public MaintenanceType(int Id, string Type, Maintenance Maintenance) : base()
        {
            this.Id = Id;
            this.Type = Type;
            this.Maintenance = Maintenance;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(10, ErrorMessage = "El nombre debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string Type { get; set; }
        public Maintenance Maintenance { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MaintenanceType type &&
                   Id == type.Id &&
                   Type == type.Type &&
                   EqualityComparer<Maintenance>.Default.Equals(Maintenance, type.Maintenance);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type, Maintenance);
        }
    }
}