using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.Models
{
    public class Maintenance
    {
        public Maintenance()
        {

        }
        public Maintenance(int Id, string Name, int NumberOfDays, float Price, IList<BookingItem> BookingItems, IList<MaintenanceType> MaintenanceTypes) : base()
        {
            this.Id = Id;
            this.Name = Name;
            this.NumberOfDays = NumberOfDays;
            this.Price = Price;
            this.BookingItems = BookingItems;
            this.MaintenanceTypes = MaintenanceTypes;
        }

        [Key]
        public int Id { get; set; }
        [StringLength(10, ErrorMessage = "El nombre debe tener menos de 10 caracteres y mas de 3.", MinimumLength = 3)]
        public string Name { get; set; }
        [Range(1, 10, ErrorMessage = "Minimo 1, Maximo 10")]
        public int NumberOfDays { get; set; }
        public float Price { get; set; }
        public IList<BookingItem> BookingItems { get; set; }
        public IList<MaintenanceType> MaintenanceTypes { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Maintenance maintenance &&
                   Id == maintenance.Id &&
                   Name == maintenance.Name &&
                   NumberOfDays == maintenance.NumberOfDays &&
                   Price == maintenance.Price &&
                   EqualityComparer<IList<BookingItem>>.Default.Equals(BookingItems, maintenance.BookingItems) &&
                   EqualityComparer<IList<MaintenanceType>>.Default.Equals(MaintenanceTypes, maintenance.MaintenanceTypes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, NumberOfDays, Price, BookingItems, MaintenanceTypes);
        }
    }
}