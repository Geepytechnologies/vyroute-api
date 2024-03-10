namespace vyroute.Models
{
    public class Transporter
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthyear { get; set; }
        public string State { get; set; }
        public string Lga { get; set; }

        public List<string> Images { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
