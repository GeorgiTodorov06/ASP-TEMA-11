namespace WebApplication6.Models
{
    public class Pacient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public int DoctorId { get; set; }
    }
}
