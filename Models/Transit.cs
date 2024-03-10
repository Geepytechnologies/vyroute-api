using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vyroute.Models
{
    public class Transit
    {
        public Guid Id { get; set; }
        public int[]? Seatsbooked { get; set; }

        public DateOnly DepartureDate { get; set; }

        public DateOnly? ArrivalDate { get; set; }

        public bool TripStarted { get; set; } = false;

        public bool TripEnded { get; set;} = false;

        public string? CurrentLocation { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [ForeignKey("TerminalId")]
        public Guid TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        [ForeignKey("VehicleID")]
        public Guid VehicleID { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
