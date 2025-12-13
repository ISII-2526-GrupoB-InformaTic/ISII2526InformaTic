using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class MaintenanceType
    {
        public MaintenanceType()
        {

        }

        public MaintenanceType(string Type)
        {
            this.Type = Type;
        }
        public MaintenanceType(int Id, string Type) : base()
        {
            this.Id = Id;
            this.Type = Type;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "El nombre debe tener menos de 30 caracteres y mas de 3.", MinimumLength = 3)]
        public string Type { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MaintenanceType type &&
                   Id == type.Id &&
                   Type == type.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }
    }
}