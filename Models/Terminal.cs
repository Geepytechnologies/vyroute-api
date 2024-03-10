namespace vyroute.Models
{
    public class Terminal
    {
        public Guid Id { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }

        public string DepartureState { get; set; }

        public string ArrivalState { get; set; }
        public int Price { get; set; }

        public int Pricediscountpercent { get; set; } = 0;

        // Navigation property for related Bookings
        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Transit> Transits { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

    }
}
