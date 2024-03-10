using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vyroute.Models
{
    public enum TripType
    {
        oneWay,
        roundTrip
    }
    public enum BookingType
    {
        carHire,
        carTravel
    }
    public enum UserType
    {
        appUser,
        guestUser
    }
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid? UserID { get; set; }

        public UserType UserType { get; set; }

        public BookingType BookingType { get; set; }
        public TripType Trip { get; set; }
        public int Amount { get; set; }
        public DateOnly DepartureDate { get; set; }

        public DateOnly? ArrivalDate { get; set; }

        public int? Passengers { get; set; }

        public int[] Seats { get; set; }

        public string NextOfKinName { get; set; }

        public string NextOfKinPhone { get; set; }

        public Payment Payment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [ForeignKey("TransitId")]
        public Guid TransitId { get; set; }
        public Transit Transit { get; set; }


        [ForeignKey("TerminalId")]
        public Guid TerminalId { get; set; }
        public Terminal Terminal { get; set; }
    }
}
