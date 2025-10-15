namespace AppForSEII2526.API.Models
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public String Name {  get; set; }

        public IList<Car> Cars { get; set; }

        public Model()
        {

        }

        public Model(int id, string name, IList<Car> car)
        {
            Id = id;
            Name = name;
            this.Cars = car;
        }

        public override bool Equals(object? obj)
        {

            return obj is Model model &&
                   Id == model.Id &&
                   Name == model.Name &&
                   Cars == model.Cars;

        }

        public override int GetHashCode()
        {

            return HashCode.Combine(Id, Name,Cars);

        }

    }

}
