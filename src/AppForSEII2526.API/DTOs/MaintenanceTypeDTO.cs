using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.API.DTOs
{
    public class MaintenanceTypeDTO
    {
        public MaintenanceTypeDTO()
        {

        }

        public MaintenanceTypeDTO(int Id, string Type) : base()
        {
            this.Id = Id;
            this.Type = Type;
        }


        [Key]
        public int Id { get; set; }
        [StringLength(10, ErrorMessage = "El nombre debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string Type { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MaintenanceTypeDTO dTO &&
                   Id == dTO.Id &&
                   Type == dTO.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }
    }
}