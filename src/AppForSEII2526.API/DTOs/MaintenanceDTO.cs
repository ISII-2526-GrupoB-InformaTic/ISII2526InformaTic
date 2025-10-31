using System.Drawing;

namespace AppForSEII2526.API.DTOs
{
    public class MaintenanceDTO
    {
        public MaintenanceDTO() { }
        public MaintenanceDTO(int Id, string Name, int NumberOfDays, float Price, IList<MaintenanceTypeDTO> MaintenanceTypes) : base()
        {
            this.Id = Id;
            this.Name = Name;
            this.NumberOfDays = NumberOfDays;
            this.Price = Price;
            this.MaintenanceTypes = MaintenanceTypes;

        }

        [Key]
        public int Id { get; set; }
        [StringLength(10, ErrorMessage = "El nombre debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string Name { get; set; }
        [Range(1, 10, ErrorMessage = "Minimo 1, Maximo 10")]
        public int NumberOfDays { get; set; }
        public float Price { get; set; }
        public IList<MaintenanceTypeDTO> MaintenanceTypes { get; set; }

    }
}
