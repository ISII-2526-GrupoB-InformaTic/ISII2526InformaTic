using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.API.DTOs
{
    public class MaintenanceTypeDTO
    {
        public MaintenanceTypeDTO()
        {

        }

        public MaintenanceTypeDTO(int Id, string Type)
        {
            this.Id = Id;
            this.Type = Type;
        }
        public MaintenanceTypeDTO(int Id, string Type, Maintenance Maintenance) : base()
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
            return obj is MaintenanceTypeDTO dTO &&
                   Id == dTO.Id &&
                   Type == dTO.Type &&
                   Maintenance == dTO.Maintenance;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type, Maintenance);
        }
    }
}