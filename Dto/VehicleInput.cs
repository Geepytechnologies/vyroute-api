using vyroute.Models;

namespace vyroute.Dto
{
    public class VehicleInput
    {
        public string VehicleNumber { get; set; }
        public string VehicleColor { get; set; }
        public VehicleType VehicleType { get; set; }
        public int VehiclePassengerCap { get; set; }


        public List<IFormFile>? Images { get; set; }

        public int Seats { get; set; }

        public Status Status { get; set; }


        public List<string>? ImagePaths { get; set; }

        public Guid TransporterId { get; set; }

        public Guid TerminalId { get; set; }
    }
}
