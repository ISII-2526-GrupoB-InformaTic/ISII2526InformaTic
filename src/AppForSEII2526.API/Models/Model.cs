namespace AppForSEII2526.API.Models
{
    public class Model
    {
        [Key]
        public int Id;

        public String Name;

        public IList<Car> Cars;

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
