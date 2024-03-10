using vyroute.Models;

namespace vyroute.Dto
{
    public class VehicleResponseDTO
    {
        public Guid Id { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleColor { get; set; }
        public VehicleType VehicleType { get; set; }

        public int VehiclePassengerCap { get; set; }

        public int Seats { get; set; }
        public Status Status { get; set; }
        public List<string> ImagePaths { get; set; }
        public List<TransitResponseDTO> Transits { get; set; }
    }
}
