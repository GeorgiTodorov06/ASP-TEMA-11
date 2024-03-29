namespace WebApplication6.Models
{
    public class Doctor
    {
         public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public List<Pacient>? Pacients { get; set; }
    }
}
