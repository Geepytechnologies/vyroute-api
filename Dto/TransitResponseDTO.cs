using vyroute.Models;

namespace vyroute.Dto
{
    public class VehicleDTO2
    {
        public Guid Id { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleColor { get; set; }
        public VehicleType VehicleType { get; set; }

        public int VehiclePassengerCap { get; set; }

        public int Seats { get; set; }
        public Status Status { get; set; }

        public List<string> ImagePaths { get; set; }
    }
    public class TransitResponseDTO
    {
        public Guid Id { get; set; }
        public int[]? Seatsbooked { get; set; }
        public int VehiclePassengerCap { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public bool TripStarted { get; set; }
        public bool TripEnded { get; set; }
        public string? CurrentLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid VehicleID { get; set; }

        public VehicleDTO2 Vehicle { get; set; }

    }
}
