using vyroute.Models;

namespace vyroute.Dto
{
    public class BookingDto
    {
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

        public Guid TerminalId { get; set; }

        public Guid TransitId { get; set; }


    }
}
