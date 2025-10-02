namespace AppForSEII2526.API.Models
{
    public class Model{

        public int Id;

        public String Name;

        public Model(){ 
        
        }


        public Model(int id){
            
            Id = id;

        }

        public Model(String name){
            
            Name = name;

        }


        public Model(int id, String name){
            Id = id;
            Name = name;

        }

        public override bool Equals(object? obj){
            
            return obj is Model model &&
                   Id == model.Id &&
                   Name == model.Name;

        }

        public override int GetHashCode(){
            
            return HashCode.Combine(Id, Name);
        
        }

    }

}
